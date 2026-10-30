using Microsoft.EntityFrameworkCore;
using Reserva2.BE.Entities;
using Reserva2.DAL;
using Reserva2.DAL.Repositories;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reserva2.BLL.Logic
{
    public class KpiResult
    {
        public decimal IngresosCobrados { get; set; }
        public decimal IngresosAnterior { get; set; }
        public decimal OcupacionPct { get; set; }
        public decimal OcupacionAnteriorPct { get; set; }
        public decimal CancelacionPct { get; set; }
        public decimal CancelacionAnteriorPct { get; set; }
        public decimal RecurrentesPct { get; set; }
        public decimal RecurrentesAnteriorPct { get; set; }
        public DateTime PeriodoAnteriorDesde { get; set; }
        public DateTime PeriodoAnteriorHasta { get; set; }
        public List<DetalleElemento> Detalle { get; set; } = new();
    }

    public class DetalleElemento
    {
        public string Elemento { get; set; } = "";
        public string Sucursal { get; set; } = "";
        public int TotalReservas { get; set; }
        public decimal OcupacionPct { get; set; }
        public decimal Ingresos { get; set; }
        public decimal CancelacionPct { get; set; }
    }

    public class ReporteBLL
    {
        private readonly ComercioRepository _comercioRepo = new();
        private readonly SucursalRepository _sucursalRepo = new();

        public List<Comercio> ListarComerciosVisibles()
        {
            var usuario = SessionManager.GetInstance().UsuarioActual
                ?? throw new Exception("Debe iniciar sesión.");
            return usuario.Rol == "Admin"
                ? _comercioRepo.GetAll()
                : _comercioRepo.GetByUsuario(usuario.IdUsuario);
        }

        public List<Sucursal> ListarSucursales(int idComercio)
        {
            return _sucursalRepo.GetByComercio(idComercio);
        }

        public KpiResult ObtenerKpis(int? idComercio, int? idSucursal, DateTime desde, DateTime hasta)
        {
            using var ctx = NegocioContextFactory.Crear();

            int dias = Math.Max(1, (int)(hasta.Date - desde.Date).TotalDays + 1);
            DateTime antHasta = desde.Date.AddDays(-1);
            DateTime antDesde = antHasta.AddDays(-(dias - 1));

            var actual = CalcularPeriodo(ctx, idComercio, idSucursal, desde.Date, hasta.Date);
            var anterior = CalcularPeriodo(ctx, idComercio, idSucursal, antDesde, antHasta);

            return new KpiResult
            {
                IngresosCobrados = actual.Ingresos,
                IngresosAnterior = anterior.Ingresos,
                OcupacionPct = actual.Ocupacion,
                OcupacionAnteriorPct = anterior.Ocupacion,
                CancelacionPct = actual.Cancelacion,
                CancelacionAnteriorPct = anterior.Cancelacion,
                RecurrentesPct = actual.Recurrentes,
                RecurrentesAnteriorPct = anterior.Recurrentes,
                PeriodoAnteriorDesde = antDesde,
                PeriodoAnteriorHasta = antHasta,
                Detalle = actual.Detalle
            };
        }

        private record PeriodoCalc(decimal Ingresos, decimal Ocupacion, decimal Cancelacion,
            decimal Recurrentes, List<DetalleElemento> Detalle);

        private static PeriodoCalc CalcularPeriodo(NegocioDbContext ctx,
            int? idComercio, int? idSucursal, DateTime desde, DateTime hasta)
        {
            var slotsQ = ctx.Disponibilidades
                .Include(d => d.Elemento)
                    .ThenInclude(e => e.Sucursal)
                .Where(d => d.Fecha >= desde && d.Fecha <= hasta);

            if (idSucursal.HasValue)
                slotsQ = slotsQ.Where(d => d.Elemento.IdSucursal == idSucursal.Value);
            else if (idComercio.HasValue)
                slotsQ = slotsQ.Where(d => d.Elemento.Sucursal.IdComercio == idComercio.Value);

            var slots = slotsQ.ToList();
            int totalSlots = slots.Count;
            int slotsOcupados = slots.Count(s => s.Estado == EstadoSlot.Reservada);

            var reservasQ = ctx.Reservas
                .Include(r => r.Disponibilidad)
                    .ThenInclude(d => d.Elemento)
                        .ThenInclude(e => e.Sucursal)
                .Where(r => r.Disponibilidad.Fecha >= desde && r.Disponibilidad.Fecha <= hasta);

            if (idSucursal.HasValue)
                reservasQ = reservasQ.Where(r => r.Disponibilidad.Elemento.IdSucursal == idSucursal.Value);
            else if (idComercio.HasValue)
                reservasQ = reservasQ.Where(r => r.Disponibilidad.Elemento.Sucursal.IdComercio == idComercio.Value);

            var reservas = reservasQ.ToList();
            int totalReservas = reservas.Count;
            int cancelYExp = reservas.Count(r =>
                r.Estado == EstadoReserva.Cancelada || r.Estado == EstadoReserva.Expirada);

            var idsReservas = reservas.Select(r => r.IdReserva).ToList();
            decimal ingresos = idsReservas.Count == 0 ? 0 :
                ctx.Pagos
                    .Where(p => idsReservas.Contains(p.IdReserva)
                        && (p.Estado == EstadoPago.Aprobado || p.Estado == EstadoPago.Reembolsado))
                    .Sum(p => (decimal?)p.Monto) ?? 0;

            var clientesGrupo = reservas
                .Where(r => r.Estado != EstadoReserva.Expirada)
                .GroupBy(r => r.IdUsuario)
                .ToList();
            int totalClientes = clientesGrupo.Count;
            int recurrentes = clientesGrupo.Count(g => g.Count() > 1);

            decimal ocupPct = totalSlots > 0 ? Math.Round(100m * slotsOcupados / totalSlots, 1) : 0;
            decimal cancelPct = totalReservas > 0 ? Math.Round(100m * cancelYExp / totalReservas, 1) : 0;
            decimal recurPct = totalClientes > 0 ? Math.Round(100m * recurrentes / totalClientes, 1) : 0;

            var detalle = reservas
                .GroupBy(r => new
                {
                    IdElemento = r.Disponibilidad.IdElemento,
                    Elemento = r.Disponibilidad.Elemento.Nombre,
                    Sucursal = r.Disponibilidad.Elemento.Sucursal.Nombre
                })
                .Select(g =>
                {
                    int totElem = g.Count();
                    int cancElem = g.Count(r => r.Estado == EstadoReserva.Cancelada || r.Estado == EstadoReserva.Expirada);
                    int slotsElem = slots.Count(s => s.IdElemento == g.Key.IdElemento);
                    int ocupElem = slots.Count(s => s.IdElemento == g.Key.IdElemento && s.Estado == EstadoSlot.Reservada);
                    var idsElem = g.Select(r => r.IdReserva).ToList();
                    decimal ingElem = idsElem.Count == 0 ? 0 :
                        ctx.Pagos
                            .Where(p => idsElem.Contains(p.IdReserva)
                                && (p.Estado == EstadoPago.Aprobado || p.Estado == EstadoPago.Reembolsado))
                            .Sum(p => (decimal?)p.Monto) ?? 0;

                    return new DetalleElemento
                    {
                        Elemento = g.Key.Elemento,
                        Sucursal = g.Key.Sucursal,
                        TotalReservas = totElem,
                        OcupacionPct = slotsElem > 0 ? Math.Round(100m * ocupElem / slotsElem, 1) : 0,
                        Ingresos = ingElem,
                        CancelacionPct = totElem > 0 ? Math.Round(100m * cancElem / totElem, 1) : 0
                    };
                })
                .OrderByDescending(d => d.Ingresos)
                .ToList();

            return new PeriodoCalc(ingresos, ocupPct, cancelPct, recurPct, detalle);
        }
    }
}
