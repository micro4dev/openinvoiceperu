﻿using Newtonsoft.Json;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using EnviarResumenResponse = OpenInvoicePeru.Comun.Dto.Intercambio.EnviarResumenResponse;

namespace OpenInvoicePeru.ClienteConsola
{
    class Program
    {
        private const string UrlSunat = "https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService";
        private const string FormatoFecha = "yyyy-MM-dd";
        private const string UrlGuiaRemision = "https://e-beta.sunat.gob.pe/ol-ti-itemision-guia-gem-beta/billService";
        private const string UrlSunatRest = "https://api-cpe.sunat.gob.pe/v1/contribuyente/gem/comprobantes";

        private const string TokenRest = "eyJraWQiOiJhcGkuc3VuYXQuZ29iLnBlLmtpZDAwMSIsInR5cCI6IkpXVCIsImFsZyI6IlJTMjU2In0.eyJzdWIiOiIyMDYxMDAxMjAzNiIsImF1ZCI6Ilt7XCJhcGlcIjpcImh0dHBzOlwvXC9hcGktY3BlLnN1bmF0LmdvYi5wZVwiLFwicmVjdXJzb1wiOlt7XCJpZFwiOlwiXC92MVwvY29udHJpYnV5ZW50ZVwvZ2VtXCIsXCJpbmRpY2Fkb3JcIjpcIjFcIixcImd0XCI6XCIxMDAwMDBcIn1dfV0iLCJ1c2VyZGF0YSI6eyJudW1SVUMiOiIyMDYxMDAxMjAzNiIsInRpY2tldCI6IjE3MTc0NDI1NDM5IiwibnJvUmVnaXN0cm8iOiIiLCJhcGVNYXRlcm5vIjoiIiwibG9naW4iOiIyMDYxMDAxMjAzNkRJTUFCRVIxIiwibm9tYnJlQ29tcGxldG8iOiJHUlVQTyBESU1BQkVSIFMuQS5DLiIsIm5vbWJyZXMiOiJHUlVQTyBESU1BQkVSIFMuQS5DLiIsImNvZERlcGVuZCI6IjAwNTMiLCJjb2RUT3BlQ29tZXIiOiIiLCJjb2RDYXRlIjoiIiwibml2ZWxVTyI6MCwiY29kVU8iOiIiLCJjb3JyZW8iOiIiLCJ1c3VhcmlvU09MIjoiRElNQUJFUjEiLCJpZCI6IiIsImRlc1VPIjoiIiwiZGVzQ2F0ZSI6IiIsImFwZVBhdGVybm8iOiIiLCJpZENlbHVsYXIiOm51bGwsIm1hcCI6eyJpc0Nsb24iOmZhbHNlLCJkZHBEYXRhIjp7ImRkcF9udW1ydWMiOiIyMDYxMDAxMjAzNiIsImRkcF9udW1yZWciOiIwMDUzIiwiZGRwX2VzdGFkbyI6IjAwIiwiZGRwX2ZsYWcyMiI6IjAwIiwiZGRwX3ViaWdlbyI6IjA0MDcwMiIsImRkcF90YW1hbm8iOiIwMyIsImRkcF90cG9lbXAiOiIzOSIsImRkcF9jaWl1IjoiNTE0MzAifSwiaWRNZW51IjoiMTcxNzQ0MjU0MzkiLCJqbmRpUG9vbCI6InAwMDUzIiwidGlwVXN1YXJpbyI6IjEiLCJ0aXBPcmlnZW4iOiJJVCIsInByaW1lckFjY2VzbyI6dHJ1ZX19LCJuYmYiOjE3MDQ2Njc0NjUsImNsaWVudElkIjoiMmE5NmEwNWEtNWYxZS00MGRmLTkyMTYtNmZkMzhlMjMwYjAzIiwiaXNzIjoiaHR0cHM6XC9cL2FwaS1zZWd1cmlkYWQuc3VuYXQuZ29iLnBlXC92MVwvY2xpZW50ZXNzb2xcLzJhOTZhMDVhLTVmMWUtNDBkZi05MjE2LTZmZDM4ZTIzMGIwM1wvb2F1dGgyXC90b2tlblwvIiwiZXhwIjoxNzA0NjcxMDY1LCJncmFudFR5cGUiOiJwYXNzd29yZCIsImlhdCI6MTcwNDY2NzQ2NX0.URlHvSdULuf-PbkUwGptf0ahxZb03q7sO8ZkyA8RZDcLi6lB5S3HJ7E5fbucLmuByCaxazwTbygkUtpE2HLMHT0xCas74sbzoITJg3ap-EkD3M7pE-aNHvVOjyq9qojvW2VE7pSHYps8_r5QoD62HBKfaNA4XSVeajhGrHDSPBw9VYPgFk1MIG4SROhIkW8hlXXJkMR5llmz8jCueTXW622vdU_mAI91109s1S8xK0jjy5uMfpf74tAbNWfU60P5TsuhCwwBiQCeuCF7L0Lyf-wt3eXJqzmkw1IuRSy8GuyDfJzp5WSYqbe2QJ7uwc0wLXixV-xwkEWWgc8ZF36csw";



        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "OpenInvoicePeru - Prueba de Envío de Documentos Electrónicos con UBL 2.1";

            CrearFacturaAlCredito();
            CrearFacturaConMuchosDecimales();
            CrearFacturaAlContado();

            CrearFacturaAlContadocertificadoPropio();


            CrearFacturaDetraccion();

            CrearFacturaGratuita();
            CrearFacturaMixta();
            CrearBoleta();
            CrearFacturaAlContadoConDscto();
            CrearFacturaAlContadoConDsctoYRedondeo();

            CrearResumenDiario();
            CrearComunicacionBaja();
            CrearNotaCredito();


            CrearNotaDebito();
            CrearNotaDebitoPorPenalidad();
            CrearNotaDebitoExonerado();

            CrearNotaCreditoConMontosGratuitos();

            CrearNotaCreditoModificacionDeFecha();

            CrearFacturaGratuitaConDscto();

            //CrearGuiaRemisionTransportePrivado();
            //CrearGuiaRemisionTransportePublico();
            //CrearGuiaRemisionTransferenciaEntreAlmacenes();
            //CrearGuiaRemisionTransferenciaEntreAlmacenes();
            //CrearGuiaRemisionCompra();
            Console.ReadLine();
        }

        private static Compania CrearEmisor()
        {
            return new Compania
            {
                NroDocumento = "20610012036",
                TipoDocumento = "6",
                NombreComercial = "DIMABER S.A.C",
                NombreLegal = "DIMABER S.A.C",
                CodigoAnexo = "0000"
            };
            //return new Compania
            //{
            //    NroDocumento = "20559170608",
            //    TipoDocumento = "6",
            //    NombreComercial = "TUCEL GROUP S.A.C.",
            //    NombreLegal = "TUCEL GROUP S.A.C.",
            //    CodigoAnexo = "0000"
            //};
        }

        private static void CrearFacturaAlCredito()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Gravada al crédito (FF11-001)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FF11-001",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = "12:00:00", //DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "01",
                    Credito = true,
                    MontoCredito = 73.74m,
                    DatoCreditos = new List<DatoCredito>()
                    {
                        new DatoCredito
                        {
                            NroCuota = 1,
                            MontoCuota = 10,
                            FechaCredito = DateTime.Today.AddDays(30).ToString(FormatoFecha),
                        },
                        new DatoCredito
                        {
                            NroCuota = 2,
                            MontoCuota = 63.75m,
                            FechaCredito = DateTime.Today.AddDays(60).ToString(FormatoFecha),
                        },
                    },
                    TotalIgv = 11.25m,
                    TotalVenta = 73.75m,
                    Gravadas = 62.50m,
                    Notas = "Este es un comentario",
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 23.60m,
                            PrecioUnitario = 20m,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 7.20m, // 
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 40m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 5,
                            PrecioReferencial = 5.31m,
                            PrecioUnitario = 4.5m,
                            TipoPrecio = "01",
                            CodigoItem = "AER345667",
                            Descripcion = "Item 2",
                            UnidadMedida = "ZZ",
                            Impuesto = 4.05m,
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 22.50m,
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearFacturaAlContado()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Al Contado (FXC1-00001010)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FXC1-00001010",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = "12:00:00", //DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "01",
                    Credito = false,
                    TotalIgv = 11.25m,
                    TotalVenta = 73.75m,
                    Gravadas = 62.50m,
                    LineExtensionAmount = 62.50m,
                    TaxInclusiveAmount = 73.75m,
                    NroOrdenCompra = "78987898",
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 23.60m,
                            PrecioUnitario = 20m,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 7.20m, // 
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 40m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 5,
                            PrecioReferencial = 5.31m,
                            PrecioUnitario = 4.5m,
                            TipoPrecio = "01",
                            CodigoItem = "AER345667",
                            Descripcion = "Item 2",
                            UnidadMedida = "ZZ",
                            Impuesto = 4.05m,
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 22.50m,
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
        private static void CrearFacturaAlContadocertificadoPropio()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Al Contado (FXC1-00001010)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FXC1-00001010",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = "12:00:00", //DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "01",
                    Credito = false,
                    TotalIgv = 11.25m,
                    TotalVenta = 73.75m,
                    Gravadas = 62.50m,
                    LineExtensionAmount = 62.50m,
                    TaxInclusiveAmount = 73.75m,
                    NroOrdenCompra = "002-3510",
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 23.60m,
                            PrecioUnitario = 20m,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 7.20m, // 
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 40m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 5,
                            PrecioReferencial = 5.31m,
                            PrecioUnitario = 4.5m,
                            TipoPrecio = "01",
                            CodigoItem = "AER345667",
                            Descripcion = "Item 2",
                            UnidadMedida = "ZZ",
                            Impuesto = 4.05m,
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 22.50m,
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearFacturaDetraccion()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Al Contado con Detraccion (FXC1-00001011)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FXC1-00001011",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = "12:00:00", //DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "USD",
                    TipoDocumento = "01",
                    TipoOperacion = "1001",
                    Credito = false,
                    TotalIgv = 11.25m,
                    TotalVenta = 73.75m,
                    Gravadas = 62.50m,
                    LineExtensionAmount = 62.50m,
                    TaxInclusiveAmount = 73.75m,
                    MontoDetraccion = 1,
                    TasaDetraccion = 0.12m,
                    CodigoBienOServicio = "022",
                    CodigoMedioPago = "001",
                    CuentaBancoNacion = "5046949",
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 23.60m,
                            PrecioUnitario = 20m,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 7.20m, // 
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 40m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 5,
                            PrecioReferencial = 5.31m,
                            PrecioUnitario = 4.5m,
                            TipoPrecio = "01",
                            CodigoItem = "AER345667",
                            Descripcion = "Item 2",
                            UnidadMedida = "ZZ",
                            Impuesto = 4.05m,
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 22.50m,
                        }
                    },
                    Leyendas = new List<Leyenda>
                    {
                        new Leyenda { Codigo = "2006", Descripcion = "Operacion Sujeta a Detraccion"}
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearFacturaGratuita()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Gratuita (FG01-00005599)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FG01-00005599",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = "12:00:00", //DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoOperacion = "0101",
                    TipoDocumento = "01",
                    Credito = false,
                    Gratuitas = 410m,
                    TasaImpuesto = 0.18m,
                    LineExtensionAmount = 0m,
                    TaxInclusiveAmount = 0m,
                    TotalIgv = 0,
                    TotalVenta = 0m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 1,
                            PrecioReferencial = 100m,
                            PrecioUnitario = 0m,
                            BaseImponible = 100m,
                            TipoPrecio = "02",
                            CodigoItem = "P001",
                            Descripcion = "Item 1",
                            UnidadMedida = "NIU",
                            Impuesto = 18m,
                            TipoImpuesto = "15",
                            TotalVenta = 100m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 1,
                            PrecioReferencial = 250m,
                            PrecioUnitario = 0m,
                            BaseImponible = 250m,
                            TipoPrecio = "02",
                            CodigoItem = "P001",
                            Descripcion = "Item 1",
                            UnidadMedida = "NIU",
                            Impuesto = 45m,
                            TipoImpuesto = "15",
                            TotalVenta = 250m,
                        },
                        new DetalleDocumento
                        {
                            Id = 3,
                            Cantidad = 1,
                            PrecioReferencial = 60m,
                            PrecioUnitario = 0m,
                            BaseImponible = 60m,
                            TipoPrecio = "02",
                            CodigoItem = "P001",
                            Descripcion = "Item 1",
                            UnidadMedida = "NIU",
                            Impuesto = 10.80m,
                            TipoImpuesto = "15",
                            TotalVenta = 60m,
                        },
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearFacturaMixta()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Mixta (FMX7-00006678)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FMX7-00006678",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = "12:00:00", //DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoOperacion = "0101",
                    TipoDocumento = "01",
                    Credito = false,
                    Gratuitas = 200m,
                    Exoneradas = 200m,
                    Gravadas = 100m,
                    TasaImpuesto = 0m,
                    LineExtensionAmount = 300m,
                    TaxInclusiveAmount = 318m,
                    TotalIgv = 18m,
                    TotalVenta = 318m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 100m,
                            PrecioUnitario = 0m,
                            BaseImponible = 200m,
                            TipoPrecio = "02",
                            CodigoItem = "P001",
                            Descripcion = "Item 1",
                            UnidadMedida = "NIU",
                            Impuesto = 0m,
                            TipoImpuesto = "21",
                            TotalVenta = 200m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 2,
                            PrecioReferencial = 100m,
                            PrecioUnitario = 100m,
                            TipoPrecio = "01",
                            CodigoItem = "P002",
                            Descripcion = "Item 2",
                            UnidadMedida = "NIU",
                            Impuesto = 0m,
                            TipoImpuesto = "20",
                            TotalVenta = 200m,
                        },
                        new DetalleDocumento
                        {
                            Id = 3,
                            Cantidad = 1,
                            PrecioReferencial = 118m,
                            PrecioUnitario = 100m,
                            TipoPrecio = "01",
                            CodigoItem = "P003",
                            Descripcion = "Item 3",
                            UnidadMedida = "NIU",
                            Impuesto = 18m,
                            TipoImpuesto = "10",
                            TotalVenta = 100m,
                        },
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearFacturaAlContadoConDscto()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Al Contado con Dcto (FX01-00001364)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FX01-00001364",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "01",
                    Credito = false,
                    TotalVenta = 62.18m,
                    Exoneradas = 70.50m,
                    LineExtensionAmount = 63.45m, //TOTAL - DESCUENTO.
                    TaxInclusiveAmount = 62.18m, //TOTAL - EL IGV DEL DESCUENTO
                    DescuentoGlobal = 7.05m,
                    FactorMultiplicadorDscto = 0.10m,
                    MontoBaseParaDcto = 70.50m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 3,
                            PrecioReferencial = 16m,
                            PrecioUnitario = 16m,
                            TipoPrecio = "01",
                            CodigoItem = "8888",
                            Descripcion = "AJI CHARAPITA",
                            UnidadMedida = "NIU",
                            Impuesto = 0m, // 
                            TipoImpuesto = "20", // Exonerada
                            TotalVenta = 48m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 5,
                            PrecioReferencial = 4.5m,
                            PrecioUnitario = 4.5m,
                            TipoPrecio = "01",
                            CodigoItem = "9999",
                            Descripcion = "CEBOLLA CHINA",
                            UnidadMedida = "NIU",
                            Impuesto = 0m,
                            TipoImpuesto = "20", // Exonerada
                            TotalVenta = 22.50m,
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearFacturaGratuitaConDscto()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Gratuita con Dcto (FDS1-00001111)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FDS1-00001111",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "01",
                    Credito = false,
                    Gravadas = 45.20m,
                    Gratuitas = 7.62m,
                    TotalVenta = 53.33m,
                    LineExtensionAmount = 45.20m, //TOTAL - DESCUENTO.
                    TaxInclusiveAmount = 53.33m, //TOTAL - EL IGV DEL DESCUENTO
                    TotalIgv = 8.13m,
                    TasaImpuesto = 0,
                    DescuentoGlobal = 6.46m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 6,
                            PrecioReferencial = 1.27m,
                            PrecioUnitario = 0m,
                            TipoPrecio = "02",
                            CodigoItem = "8888",
                            Descripcion = "ITEM 01",
                            UnidadMedida = "NIU",
                            Impuesto = 0.22m, // 
                            TipoImpuesto = "15", // Bonificacion Gravada
                            TotalVenta = 7.62m,
                            BaseImponible = 7.62m
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 48,
                            PrecioReferencial = 1.27m,
                            PrecioUnitario = 1.076271m,
                            TipoPrecio = "01",
                            CodigoItem = "9999",
                            Descripcion = "ITEM 02",
                            UnidadMedida = "NIU",
                            Impuesto = 9.3m,
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 51.66m,
                            BaseImponible = 51.66m
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearFacturaAlContadoConDsctoYRedondeo()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Al Contado con Descuento y Redondeo (FXD1-00001999)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FXD1-00001999",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "01",
                    Credito = false,
                    TotalVenta = 57.05m,
                    Exoneradas = 60m,
                    LineExtensionAmount = 57m,
                    TaxInclusiveAmount = 57m,
                    DescuentoGlobal = 3m,
                    Redondeo = 0.05m,
                    //FactorMultiplicadorDscto = 0.10m,
                    //MontoBaseParaDcto = 102.50m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 20m,
                            PrecioUnitario = 20m,
                            TipoPrecio = "01",
                            CodigoItem = "8888",
                            Descripcion = "AJI CHARAPITA",
                            UnidadMedida = "NIU",
                            Impuesto = 0m, // 
                            TipoImpuesto = "20", // Exonerada
                            TotalVenta = 40m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 1,
                            PrecioReferencial = 20m,
                            PrecioUnitario = 20m,
                            TipoPrecio = "01",
                            CodigoItem = "9999",
                            Descripcion = "CEBOLLA CHINA",
                            UnidadMedida = "NIU",
                            Impuesto = 0m,
                            TipoImpuesto = "20", // Exonerada
                            TotalVenta = 20m,
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearFacturaConMuchosDecimales()
        {
            try
            {
                Console.WriteLine("Ejemplo Factura Gravada con muchos decimales (FX50-100)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FX50-100",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "01",
                    Credito = false,
                    TotalIgv = 305.08m,
                    TotalVenta = 2000m,
                    Gravadas = 1694.914m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 20000,
                            PrecioReferencial = 0.1m,
                            PrecioUnitario = 0.0847460m,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 305.085600m, // 
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 1694.920000m
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearBoleta()
        {
            try
            {
                Console.WriteLine("Ejemplo Boleta Gravada al Contado (BB11-008)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "88888888",
                        TipoDocumento = "1",
                        NombreLegal = "CLIENTES VARIOS"
                    },
                    IdDocumento = "BB11-008",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = "12:00:00", //DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "03",
                    TotalIgv = 11.25m,
                    TotalVenta = 73.75m,
                    Gravadas = 62.50m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 23.60m,
                            PrecioUnitario = 20m,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 7.20m, // 
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 40m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 5,
                            PrecioReferencial = 5.31m,
                            PrecioUnitario = 4.5m,
                            TipoPrecio = "01",
                            CodigoItem = "AER345667",
                            Descripcion = "Item 2",
                            UnidadMedida = "ZZ",
                            Impuesto = 4.05m,
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 22.50m,
                        },
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearNotaCredito()
        {
            try
            {
                Console.WriteLine("Ejemplo Nota de Crédito de Factura (FC01-00000178)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20335955065",
                        TipoDocumento = "6",
                        NombreLegal = "MEDIA NETWORKS LATIN AMERICA S.A.C.",
                        CodigoAnexo = ""
                    },
                    IdDocumento = "FC01-00000178",
                    FechaEmision = DateTime.Today.AddDays(-5).ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    FechaVencimiento = "2021-12-29",
                    MontoEnLetras = string.Empty,
                    Moneda = "PEN",
                    TipoDocumento = "07",
                    TotalIgv = 11.25m,
                    TotalVenta = 73.75m,
                    Gravadas = 62.50m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 23.60m,
                            PrecioUnitario = 20m,
                            BaseImponible = 40m,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 7.20m, // 
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 40m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 5,
                            PrecioReferencial = 5.31m,
                            PrecioUnitario = 4.5m,
                            BaseImponible = 22.50m,
                            TipoPrecio = "01",
                            CodigoItem = "AER345667",
                            Descripcion = "Item 2",
                            UnidadMedida = "ZZ",
                            Impuesto = 4.05m,
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 22.50m,
                        }
                    },
                    Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            NroReferencia = "FM01-00001318",
                            Tipo = "01",
                            Descripcion = "CANCELACION TOTAL"
                        }
                    },
                    //Relacionados = new List<DocumentoRelacionado>
                    //{
                    //    new DocumentoRelacionado
                    //    {
                    //        NroDocumento = "FF11-001",
                    //        TipoDocumento = "01"
                    //    }
                    //}
                };

                File.WriteAllText("notacredito.json", JsonConvert.SerializeObject(documento));

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearNotaCreditoConMontosGratuitos()
        {
            try
            {
                Console.WriteLine("Ejemplo Nota de Credito con Montos Gratuitos (FMC1-00009900)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FMC1-00009900",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoOperacion = "0101",
                    TipoDocumento = "07",
                    Credito = false,
                    Gratuitas = 200m,
                    Exoneradas = 200m,
                    Gravadas = 0m,
                    TasaImpuesto = 0.0m,
                    LineExtensionAmount = 200,
                    TaxInclusiveAmount = 200,
                    TotalIgv = 0,
                    TotalVenta = 200,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 100m,
                            PrecioUnitario = 0m,
                            BaseImponible = 200m,
                            TipoPrecio = "02",
                            CodigoItem = "P001",
                            Descripcion = "Item 1",
                            UnidadMedida = "NIU",
                            Impuesto = 0m,
                            TipoImpuesto = "21",
                            TotalVenta = 200m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 2,
                            PrecioReferencial = 100m,
                            PrecioUnitario = 100m,
                            BaseImponible = 200m,
                            TipoPrecio = "01",
                            CodigoItem = "P002",
                            Descripcion = "Item 2",
                            UnidadMedida = "NIU",
                            Impuesto = 0m,
                            TipoImpuesto = "20",
                            TotalVenta = 200m,
                        }
                    },
                    Relacionados = new List<DocumentoRelacionado>
                    {
                        new DocumentoRelacionado
                        {
                            NroDocumento = "F001-00165004",
                            TipoDocumento = "01",
                        }
                    },
                    Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            Descripcion = "ERROR EN LA OPERACION",
                            NroReferencia = "F001-00165004",
                            Tipo = "01"
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearNotaCreditoModificacionDeFecha()
        {
            try
            {
                Console.WriteLine("Ejemplo Nota de Credito con Montos Gratuitos (FMC1-00009900)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20100121809",
                        TipoDocumento = "6",
                        NombreLegal = "ADMINISTRADORA CLINICA RICARDO PALMA S.A."
                    },
                    IdDocumento = "FMC1-00009900",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoOperacion = "0101",
                    TipoDocumento = "07",
                    Credito = true,
                    Gratuitas = 0m,
                    Exoneradas = 0m,
                    Gravadas = 0m,
                    TasaImpuesto = 0.0m,
                    LineExtensionAmount = 0m,
                    TaxInclusiveAmount = 0m,
                    TotalIgv = 0,
                    TotalVenta = 0,

                    MontoCredito = 90.00m,
                    DatoCreditos = new List<DatoCredito>()
                    {
                        new DatoCredito
                        {
                            NroCuota = 1,
                            MontoCuota = 90,
                            FechaCredito = DateTime.Today.AddDays(30).ToString(FormatoFecha),
                        },

                    },

                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 1,
                            PrecioReferencial = 0m,
                            PrecioUnitario = 0m,
                            BaseImponible = 0m,
                            TipoPrecio = "02",
                            CodigoItem = "P001",
                            Descripcion = "Item 1",
                            UnidadMedida = "NIU",
                            Impuesto = 0m,
                            TipoImpuesto = "21",
                            TotalVenta = 0m
                        }

                    },
                    Relacionados = new List<DocumentoRelacionado>
                    {
                        new DocumentoRelacionado
                        {
                            NroDocumento = "F001-00165004",
                            TipoDocumento = "01",
                        }
                    },
                    Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            Descripcion = "CAMBIO DE FECHA DE PAGO",
                            NroReferencia = "F001-00165004",
                            Tipo = "13"
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
        private static void CrearNotaDebito()
        {
            try
            {
                Console.WriteLine("Ejemplo Nota de Débito de Factura (FD11-001)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20257471609",
                        TipoDocumento = "6",
                        NombreLegal = "FRAMEWORK PERU"
                    },
                    IdDocumento = "FD11-001",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "08",
                    TotalIgv = 11.25m,
                    TotalVenta = 73.75m,
                    Gravadas = 62.50m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 23.60m,
                            PrecioUnitario = 20m,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 7.20m, // 
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 40m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 5,
                            PrecioReferencial = 5.31m,
                            PrecioUnitario = 4.5m,
                            TipoPrecio = "01",
                            CodigoItem = "AER345667",
                            Descripcion = "Item 2",
                            UnidadMedida = "ZZ",
                            Impuesto = 4.05m,
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 22.50m,
                        }
                    },
                    Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            NroReferencia = "FF11-001",
                            Tipo = "03",
                            Descripcion = "Penalidad por falta de pago"
                        }
                    },
                    Relacionados = new List<DocumentoRelacionado>
                    {
                        new DocumentoRelacionado
                        {
                            NroDocumento = "FF11-001",
                            TipoDocumento = "01"
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearNotaDebitoPorPenalidad()
        {
            try
            {
                Console.WriteLine("Ejemplo Nota de Débito de Factura (FD11-002)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20257471609",
                        TipoDocumento = "6",
                        NombreLegal = "FRAMEWORK PERU"
                    },
                    IdDocumento = "FD11-002",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "08",
                    TotalIgv = 11.25m,
                    TotalVenta = 73.75m,
                    Gravadas = 62.50m,
                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 2,
                            PrecioReferencial = 23.60m,
                            PrecioUnitario = 20m,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 7.20m, // 
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 40m,
                        },
                        new DetalleDocumento
                        {
                            Id = 2,
                            Cantidad = 5,
                            PrecioReferencial = 5.31m,
                            PrecioUnitario = 4.5m,
                            TipoPrecio = "01",
                            CodigoItem = "AER345667",
                            Descripcion = "Item 2",
                            UnidadMedida = "ZZ",
                            Impuesto = 4.05m,
                            TipoImpuesto = "10", // Gravada
                            TotalVenta = 22.50m,
                        }
                    },
                    Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            Tipo = "03",
                            Descripcion = "Penalidad por falta de pago"
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearNotaDebitoExonerado()
        {
            try
            {
                Console.WriteLine("Ejemplo Nota de Débito Exonerada de Factura (FD11-001)");
                var documento = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(),
                    Receptor = new Compania
                    {
                        NroDocumento = "20257471609",
                        TipoDocumento = "6",
                        NombreLegal = "FRAMEWORK PERU"
                    },
                    IdDocumento = "FD12-001",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    Moneda = "PEN",
                    TipoDocumento = "08",
                    TotalIgv = 0m,
                    TotalVenta = 250m,
                    Gravadas = 0m,
                    Exoneradas = 250m,
                    TasaImpuesto = 0.0m,
                    LineExtensionAmount = 0m,
                    TaxInclusiveAmount = 0m,

                    Items = new List<DetalleDocumento>
                    {
                        new DetalleDocumento
                        {
                            Id = 1,
                            Cantidad = 5,
                            PrecioReferencial = 50m,
                            PrecioUnitario = 50m,
                            BaseImponible = 250,
                            TipoPrecio = "01",
                            CodigoItem = "1234234",
                            Descripcion = "Item 1",
                            UnidadMedida = "ZZ",
                            Impuesto = 0m, // 
                            TipoImpuesto = "21", // Exonerada
                            TotalVenta = 250m,
                        },
                    },
                    Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            NroReferencia = "FF11-001",
                            Tipo = "03",
                            Descripcion = "Penalidad por falta de pago"
                        }
                    },
                    Relacionados = new List<DocumentoRelacionado>
                    {
                        new DocumentoRelacionado
                        {
                            NroDocumento = "FF11-001",
                            TipoDocumento = "01"
                        }
                    }
                };

                FirmaryEnviar(documento, GenerarDocumento(documento));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearResumenDiario()
        {
            try
            {
                Console.WriteLine("Ejemplo de Resumen Diario");
                var documentoResumenDiario = new ResumenDiarioNuevo
                {
                    IdDocumento = $"RC-{DateTime.Today:yyyyMMdd}-001",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    FechaReferencia = DateTime.Today.AddDays(-1).ToString(FormatoFecha),
                    Emisor = CrearEmisor(),
                    Resumenes = new List<GrupoResumenNuevo>()
                };

                documentoResumenDiario.Resumenes.Add(new GrupoResumenNuevo
                {
                    Id = 1,
                    TipoDocumento = "03",
                    IdDocumento = "BB14-33386",
                    NroDocumentoReceptor = "41614074",
                    TipoDocumentoReceptor = "1",
                    CodigoEstadoItem = 1, // 1 - Agregar. 2 - Modificar. 3 - Eliminar
                    Moneda = "PEN",
                    TotalVenta = 190.9m,
                    TotalIgv = 29.12m,
                    TotalImpuestoBolsas = 6.50m,
                    Gravadas = 161.78m,
                });
                // Para los casos de envio de boletas anuladas, se debe primero informar las boletas creadas (1) y luego en un segundo resumen se envian las anuladas. De lo contrario se presentará el error 'El documento indicado no existe no puede ser modificado/eliminado'
                documentoResumenDiario.Resumenes.Add(new GrupoResumenNuevo
                {
                    Id = 2,
                    TipoDocumento = "03",
                    IdDocumento = "BB30-33384",
                    NroDocumentoReceptor = "08506678",
                    TipoDocumentoReceptor = "1",
                    CodigoEstadoItem = 1, // 1 - Agregar. 2 - Modificar. 3 - Eliminar
                    Moneda = "USD",
                    TotalVenta = 9580m,
                    TotalIgv = 1411.36m,
                    Gravadas = 8168.64m,
                });


                Console.WriteLine("Generando XML....");

                var documentoResponse = RestHelper<ResumenDiarioNuevo, DocumentoResponse>.Execute("GenerarResumenDiario/v2", documentoResumenDiario);

                if (!documentoResponse.Exito)
                    throw new InvalidOperationException(documentoResponse.MensajeError);

                Console.WriteLine("Firmando XML...");
                // Firmado del Documento.
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("Certificado.pfx")),
                    PasswordCertificado = "nEu9YayAtTAyrMVcu",
                };

                var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

                if (!responseFirma.Exito)
                {
                    throw new InvalidOperationException(responseFirma.MensajeError);
                }

                Console.WriteLine("Guardando XML de Resumen....(Revisar carpeta del ejecutable)");

                File.WriteAllBytes("resumendiario.xml", Convert.FromBase64String(responseFirma.TramaXmlFirmado));

                Console.WriteLine("Enviando a SUNAT....");

                var enviarDocumentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = documentoResumenDiario.Emisor.NroDocumento,
                    UsuarioSol = "MODDATOS",
                    ClaveSol = "MODDATOS",
                    EndPointUrl = UrlSunat,
                    IdDocumento = documentoResumenDiario.IdDocumento,
                    TramaXmlFirmado = responseFirma.TramaXmlFirmado
                };

                var enviarResumenResponse = RestHelper<EnviarDocumentoRequest, EnviarResumenResponse>.Execute("EnviarResumen", enviarDocumentoRequest);

                if (!enviarResumenResponse.Exito)
                {
                    throw new InvalidOperationException(enviarResumenResponse.MensajeError);
                }

                Console.WriteLine("Nro de Ticket: {0}", enviarResumenResponse.NroTicket);

                ConsultarTicket(enviarResumenResponse.NroTicket, documentoResumenDiario.Emisor.NroDocumento);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void CrearComunicacionBaja()
        {
            try
            {
                Console.WriteLine("Ejemplo de Comunicación de Baja");
                var documentoBaja = new ComunicacionBaja
                {
                    IdDocumento = $"RA-{DateTime.Today:yyyyMMdd}-001",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    FechaReferencia = DateTime.Today.AddDays(-1).ToString(FormatoFecha),
                    Emisor = CrearEmisor(),
                    Bajas = new List<DocumentoBaja>()
                };

                // En las comunicaciones de Baja ya no se pueden colocar boletas, ya que la anulacion de las mismas
                // la realiza el resumen diario.
                documentoBaja.Bajas.Add(new DocumentoBaja
                {
                    Id = 1,
                    Correlativo = "33386",
                    TipoDocumento = "01",
                    Serie = "FA50",
                    MotivoBaja = "Anulación por otro tipo de documento"
                });
                documentoBaja.Bajas.Add(new DocumentoBaja
                {
                    Id = 2,
                    Correlativo = "86486",
                    TipoDocumento = "01",
                    Serie = "FF14",
                    MotivoBaja = "Anulación por otro datos erroneos"
                });

                Console.WriteLine("Generando XML....");

                var documentoResponse = RestHelper<ComunicacionBaja, DocumentoResponse>.Execute("GenerarComunicacionBaja", documentoBaja);
                if (!documentoResponse.Exito)
                {
                    throw new InvalidOperationException(documentoResponse.MensajeError);
                }

                Console.WriteLine("Firmando XML...");
                // Firmado del Documento.
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("Certificado.pfx")),
                    PasswordCertificado = "nEu9YayAtTAyrMVcu",
                };

                var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

                if (!responseFirma.Exito)
                {
                    throw new InvalidOperationException(responseFirma.MensajeError);
                }

                Console.WriteLine("Guardando XML de la Comunicacion de Baja....(Revisar carpeta del ejecutable)");

                File.WriteAllBytes("comunicacionbaja.xml", Convert.FromBase64String(responseFirma.TramaXmlFirmado));

                Console.WriteLine("Enviando a SUNAT....");

                var sendBill = new EnviarDocumentoRequest
                {
                    Ruc = documentoBaja.Emisor.NroDocumento,
                    UsuarioSol = "MODDATOS",
                    ClaveSol = "MODDATOS",
                    EndPointUrl = UrlSunat,
                    IdDocumento = documentoBaja.IdDocumento,
                    TramaXmlFirmado = responseFirma.TramaXmlFirmado
                };

                var enviarResumenResponse = RestHelper<EnviarDocumentoRequest, EnviarResumenResponse>.Execute("EnviarResumen", sendBill);

                if (!enviarResumenResponse.Exito)
                {
                    throw new InvalidOperationException(enviarResumenResponse.MensajeError);
                }

                Console.WriteLine("Nro de Ticket: {0}", enviarResumenResponse.NroTicket);

                ConsultarTicket(enviarResumenResponse.NroTicket, documentoBaja.Emisor.NroDocumento);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void ConsultarTicket(string nroTicket, string nroRuc)
        {
            var consultarTicketRequest = new ConsultaTicketRequest
            {
                Ruc = nroRuc,
                NroTicket = nroTicket,
                UsuarioSol = "MODDATOS",
                ClaveSol = "MODDATOS",
                EndPointUrl = UrlSunat
            };

            var response = RestHelper<ConsultaTicketRequest, EnviarDocumentoResponse>.Execute("ConsultarTicket", consultarTicketRequest);

            if (!response.Exito)
            {
                Console.WriteLine($"{response.MensajeError} {response.Pila}");
                return;
            }

            var archivo = response.NombreArchivo.Replace(".xml", string.Empty);
            Console.WriteLine($"Escribiendo documento en la carpeta del ejecutable... {archivo}");

            File.WriteAllBytes($"{archivo}.zip", Convert.FromBase64String(response.TramaZipCdr));

            Console.WriteLine($"Código: {response.CodigoRespuesta} => {response.MensajeRespuesta}");
        }

        private static void ConsultarTicketRest(string nroTicket, string nroRuc)
        {
            var consultarTicketRequest = new ConsultaTicketRequest
            {
                Ruc = nroRuc,
                NroTicket = nroTicket,
                UsuarioSol = "MODDATOS",
                ClaveSol = "MODDATOS",
                EndPointUrl = UrlSunatRest,
                Token = TokenRest
            };

            var response = RestHelper<ConsultaTicketRequest, EnviarDocumentoResponse>.Execute("ConsultarTicketRest", consultarTicketRequest);

            if (!response.Exito)
            {
                Console.WriteLine($"{response.MensajeError} {response.Pila}");
                return;
            }

            if (response.CodigoRespuesta.Equals("0"))
            {

                File.WriteAllBytes($"{nroTicket}.zip", Convert.FromBase64String(response.TramaZipCdr));
                Console.WriteLine($"Código: {response.CodigoRespuesta} => {response.MensajeRespuesta}");
            }
            else
            {
                Console.WriteLine($"Código: {response.CodigoRespuesta}");
                Console.WriteLine($"Mensaje: {response.MensajeRespuesta}");
                Console.WriteLine($"Error: {response.MensajeError}");

            }

            //var archivo = response.NombreArchivo.Replace(".xml", string.Empty);
            //Console.WriteLine($"Escribiendo documento en la carpeta del ejecutable... {archivo}");

        }

        private static void CrearGuiaRemisionTransportePrivado()
        {
            try
            {
                Console.WriteLine("Ejemplo de Guia de Remisión");
                var guia = new GuiaRemision
                {
                    IdDocumento = "T000-00000007",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    TipoDocumento = "09",
                    Glosa = "Guia de Prueba",
                    Remitente = CrearEmisor(),
                    Destinatario = new Contribuyente
                    {
                        NroDocumento = "10432639687",
                        TipoDocumento = "6",
                        NombreLegal = "CARLOS ERNESTO ILLACUTIPA ESPINOZA",
                    },
                    ShipmentId = "001",
                    CodigoMotivoTraslado = "02",
                    DescripcionMotivo = "OTRO OTRO",
                    Transbordo = false,
                    PesoBrutoTotal = 50,
                    NroPallets = 0,
                    ModalidadTraslado = "02", //Privado
                    FechaInicioTraslado = DateTime.Today.ToString(FormatoFecha),
                    RucTransportista = "10432639687",
                    RazonSocialTransportista = "FRAMEWORK PERU",
                    NroMtc = "0001",
                    NroPlacaVehiculo = "V0Q504",
                    NumeroContenedor = "V0Q504",
                    NroDocumentoConductor = "41075490",
                    NombreConductor = "CHOFER CARLOS",
                    NroLicenciaConductor = "H41075490",
                    DocumentoRelacionado = new DocumentoRelacionado
                    {
                        TipoDocumento = "01",
                        NroDocumento = "F001-123",
                        RucEmisorDocumentoRelaciondo = "20610012036"
                    },
                    DireccionPartida = new Direccion
                    {
                        Ubigeo = "150101",
                        DireccionCompleta = "AV LIMA 455"
                    },
                    DireccionLlegada = new Direccion
                    {
                        Ubigeo = "160101",
                        DireccionCompleta = "AV ITALIA 555"
                    },
                    CodigoPuerto = string.Empty,

                    BienesATransportar = new List<DetalleGuia>()
                {
                    new DetalleGuia
                    {
                        Correlativo = 1,
                        CodigoItem = "0001",
                        Descripcion = "CHIP",
                        UnidadMedida = "NIU",
                        Cantidad = 4,
                        LineaReferencia = 1
                    }
                },

                };

                Console.WriteLine("Generando XML....");

                var documentoResponse = RestHelper<GuiaRemision, DocumentoResponse>.Execute("GenerarGuiaRemision", guia);

                if (!documentoResponse.Exito)
                {
                    throw new InvalidOperationException(documentoResponse.MensajeError);
                }



                var json = JsonConvert.SerializeObject(guia, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                File.WriteAllText($"{guia.IdDocumento}.json", json);


                Console.WriteLine("Firmando XML...");
                // Firmado del Documento.
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("certificado.pfx")),
                    PasswordCertificado = "nEu9YayAtTAyrMVcu",
                };

                var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

                if (!responseFirma.Exito)
                {
                    throw new InvalidOperationException(responseFirma.MensajeError);
                }

                File.WriteAllBytes("20559170608-09-" + guia.IdDocumento + ".xml", Convert.FromBase64String(responseFirma.TramaXmlFirmado));

                Console.WriteLine("Enviando a SUNAT....");

                var documentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = guia.Remitente.NroDocumento,
                    UsuarioSol = "DIMABER1",
                    ClaveSol = "GaTeUeGo1",
                    EndPointUrl = UrlSunatRest,
                    IdDocumento = guia.IdDocumento,
                    TipoDocumento = guia.TipoDocumento,
                    TramaXmlFirmado = responseFirma.TramaXmlFirmado,
                    Token = TokenRest
                };

                var enviarDocumentoResponse = RestHelper<EnviarDocumentoRequest, EnviarResumenResponse>.Execute("EnviarDocumentoRest", documentoRequest);


                if (!enviarDocumentoResponse.Exito)
                {
                    throw new InvalidOperationException(enviarDocumentoResponse.MensajeError);
                }

                //File.WriteAllBytes("GuaiRemisionCdr.zip", Convert.FromBase64String(enviarDocumentoResponse.TramaZipCdr));

                Console.WriteLine("Respuesta de SUNAT Ticket:");
                Console.WriteLine(enviarDocumentoResponse.NroTicket);

                ConsultarTicketRest(enviarDocumentoResponse.NroTicket, documentoRequest.Ruc);
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }

        }
        private static void CrearGuiaRemisionTransportePublico()
        {
            try
            {
                Console.WriteLine("Ejemplo de Guia de Remisión");
                var guia = new GuiaRemision
                {
                    IdDocumento = "T000-00000022",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    TipoDocumento = "09",
                    Glosa = "Guia de Prueba",
                    Remitente = CrearEmisor(),
                    Destinatario = new Contribuyente
                    {
                        NroDocumento = "20455550115",
                        TipoDocumento = "6",
                        NombreLegal = "CORP TEG SAC",
                    },
                    ShipmentId = "001",
                    CodigoMotivoTraslado = "01",
                    DescripcionMotivo = "VENTA",
                    Transbordo = false,
                    PesoBrutoTotal = 50,
                    NroPallets = 0,
                    ModalidadTraslado = "01", //Publico
                    FechaInicioTraslado = DateTime.Today.ToString(FormatoFecha),
                    RucTransportista = "10432639687",
                    RazonSocialTransportista = "TRANSPORTES S.A.C",
                    NroMtc = "0001",
                    NroPlacaVehiculo = "ABC123",
                    NumeroContenedor = "ABI456",
                    NroDocumentoConductor = "43263968",
                    NombreConductor = "CHOFER CARLOS",
                    NroLicenciaConductor = "43263968",
                    DireccionPartida = new Direccion
                    {
                        Ubigeo = "150101",
                        DireccionCompleta = "AV LIMA 455"
                    },
                    DireccionLlegada = new Direccion
                    {
                        Ubigeo = "160101",
                        DireccionCompleta = "AV ITALIA 555"
                    },
                    CodigoPuerto = string.Empty,
                    BienesATransportar = new List<DetalleGuia>()
                {
                    new DetalleGuia
                    {
                        Correlativo = 1,
                        CodigoItem = "0001",
                        Descripcion = "CHIP",
                        UnidadMedida = "NIU",
                        Cantidad = 4,
                        LineaReferencia = 1
                    }
                }
                };

                Console.WriteLine("Generando XML....");

                var documentoResponse = RestHelper<GuiaRemision, DocumentoResponse>.Execute("GenerarGuiaRemision", guia);

                if (!documentoResponse.Exito)
                {
                    throw new InvalidOperationException(documentoResponse.MensajeError);
                }

                Console.WriteLine("Firmando XML...");
                // Firmado del Documento.
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("certificado.pfx")),
                    PasswordCertificado = "nEu9YayAtTAyrMVcu",
                };

                var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

                if (!responseFirma.Exito)
                {
                    throw new InvalidOperationException(responseFirma.MensajeError);
                }

                File.WriteAllBytes("20559170608-09-" + guia.IdDocumento + ".xml", Convert.FromBase64String(responseFirma.TramaXmlFirmado));

                Console.WriteLine("Enviando a SUNAT....");

                var documentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = guia.Remitente.NroDocumento,
                    UsuarioSol = "TUCELFE1",
                    ClaveSol = "CommonApp1",
                    EndPointUrl = UrlSunatRest,
                    IdDocumento = guia.IdDocumento,
                    TipoDocumento = guia.TipoDocumento,
                    TramaXmlFirmado = responseFirma.TramaXmlFirmado,
                    Token = TokenRest
                };

                var enviarDocumentoResponse = RestHelper<EnviarDocumentoRequest, EnviarResumenResponse>.Execute("EnviarDocumentoRest", documentoRequest);

                if (!enviarDocumentoResponse.Exito)
                {
                    throw new InvalidOperationException(enviarDocumentoResponse.MensajeError);
                }

                //File.WriteAllBytes("GuaiRemisionCdr.zip", Convert.FromBase64String(enviarDocumentoResponse.TramaZipCdr));

                //Console.WriteLine("Respuesta de SUNAT:");
                //Console.WriteLine(enviarDocumentoResponse.MensajeRespuesta);

                Console.WriteLine("Respuesta de SUNAT Ticket:");
                Console.WriteLine(enviarDocumentoResponse.NroTicket);

                ConsultarTicketRest(enviarDocumentoResponse.NroTicket, documentoRequest.Ruc);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }

        }

        private static void CrearGuiaRemisionTransferenciaEntreAlmacenes()
        {
            try
            {
                Console.WriteLine("Ejemplo de Guia de Remisión");
                var guia = new GuiaRemision
                {
                    IdDocumento = "T111-00000001",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    TipoDocumento = "09",
                    Glosa = "Guia de Prueba",
                    Remitente = CrearEmisor(),
                    Destinatario = new Contribuyente
                    {
                        NroDocumento = "20610012036",
                        TipoDocumento = "6",
                        NombreLegal = "DIMABER S.A.C",
                    },
                    ShipmentId = "001",
                    CodigoMotivoTraslado = "04",
                    DescripcionMotivo = "TRASLADO ENTRE ESTABLECIMIENTO DE UNA MISMA EMPRESA",
                    Transbordo = false,
                    PesoBrutoTotal = 50,
                    NroPallets = 0,
                    ModalidadTraslado = "01", //Publico
                    FechaInicioTraslado = DateTime.Today.ToString(FormatoFecha),
                    RucTransportista = "10432639687",
                    RazonSocialTransportista = "TRANSPORTES S.A.C",
                    NroMtc = "0001",
                    NroPlacaVehiculo = "ABC123",
                    NumeroContenedor = "ABI456",
                    NroDocumentoConductor = "43263968",
                    NombreConductor = "CHOFER CARLOS",
                    NroLicenciaConductor = "43263968",
                    DireccionPartida = new Direccion
                    {
                        Ubigeo = "150101",
                        DireccionCompleta = "AV LIMA 455",
                        CodigoAnexo = "0000"
                    },
                    DireccionLlegada = new Direccion
                    {
                        Ubigeo = "160101",
                        DireccionCompleta = "AV ITALIA 555",
                        CodigoAnexo = "0000"
                    },
                    CodigoPuerto = string.Empty,
                    BienesATransportar = new List<DetalleGuia>()
                {
                    new DetalleGuia
                    {
                        Correlativo = 1,
                        CodigoItem = "0001",
                        Descripcion = "CHIP",
                        UnidadMedida = "NIU",
                        Cantidad = 4,
                        LineaReferencia = 1
                    }
                }
                };

                Console.WriteLine("Generando XML....");

                var documentoResponse = RestHelper<GuiaRemision, DocumentoResponse>.Execute("GenerarGuiaRemision", guia);

                if (!documentoResponse.Exito)
                {
                    throw new InvalidOperationException(documentoResponse.MensajeError);
                }

                Console.WriteLine("Firmando XML...");
                // Firmado del Documento.
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("certificado.pfx")),
                    PasswordCertificado = "nEu9YayAtTAyrMVcu",
                };

                var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

                if (!responseFirma.Exito)
                {
                    throw new InvalidOperationException(responseFirma.MensajeError);
                }

                File.WriteAllBytes("20610012036-09-" + guia.IdDocumento + ".xml", Convert.FromBase64String(responseFirma.TramaXmlFirmado));

                Console.WriteLine("Enviando a SUNAT....");

                var documentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = guia.Remitente.NroDocumento,
                    UsuarioSol = "DIMABER1",
                    ClaveSol = "GaTeUeGo1",
                    EndPointUrl = UrlSunatRest,
                    IdDocumento = guia.IdDocumento,
                    TipoDocumento = guia.TipoDocumento,
                    TramaXmlFirmado = responseFirma.TramaXmlFirmado,
                    Token = TokenRest
                };

                var enviarDocumentoResponse = RestHelper<EnviarDocumentoRequest, EnviarResumenResponse>.Execute("EnviarDocumentoRest", documentoRequest);

                if (!enviarDocumentoResponse.Exito)
                {
                    throw new InvalidOperationException(enviarDocumentoResponse.MensajeError);
                }

                //File.WriteAllBytes("GuaiRemisionCdr.zip", Convert.FromBase64String(enviarDocumentoResponse.TramaZipCdr));

                //Console.WriteLine("Respuesta de SUNAT:");
                //Console.WriteLine(enviarDocumentoResponse.MensajeRespuesta);

                Console.WriteLine("Respuesta de SUNAT Ticket:");
                Console.WriteLine(enviarDocumentoResponse.NroTicket);

                ConsultarTicketRest(enviarDocumentoResponse.NroTicket, documentoRequest.Ruc);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }

        }
        private static void CrearGuiaRemisionCompra()
        {
            try
            {
                Console.WriteLine("Ejemplo de Guia de Remisión  de compra");
                var guia = new GuiaRemision
                {
                    IdDocumento = "T000-00000069",
                    FechaEmision = DateTime.Today.ToString(FormatoFecha),
                    HoraEmision = DateTime.Now.ToString("HH:mm:ss"),
                    TipoDocumento = "09",
                    Glosa = "Guia de Prueba",
                    Remitente = CrearEmisor(),
                    Destinatario = new Contribuyente
                    {
                        NroDocumento = "20610012036",
                        TipoDocumento = "6",
                        NombreLegal = "DIMABER S.A.C",
                    },
                    Tercero = new Contribuyente()
                    {
                        NroDocumento = "10432639687",
                        TipoDocumento = "6",
                        NombreLegal = "CARLOS ERNESTO",
                    },
                    ShipmentId = "001",
                    CodigoMotivoTraslado = "02",
                    DescripcionMotivo = "COMPRA",
                    Transbordo = false,
                    PesoBrutoTotal = 50,
                    NroPallets = 0,
                    ModalidadTraslado = "01", //Publico
                    FechaInicioTraslado = DateTime.Today.ToString(FormatoFecha),
                    RucTransportista = "10432639687",
                    RazonSocialTransportista = "TRANSPORTES S.A.C",
                    NroMtc = "0001",
                    NroPlacaVehiculo = "ABC123",
                    NumeroContenedor = "ABI456",
                    NroDocumentoConductor = "43263968",
                    NombreConductor = "CHOFER CARLOS",
                    NroLicenciaConductor = "43263968",
                    DocumentoRelacionado = new DocumentoRelacionado()
                    {
                        NroDocumento = "F001-00000001",
                        TipoDocumento = "01",
                        DescripcionTipoDocumento = "Factura",
                        RucEmisorDocumentoRelaciondo = "10432639687",
                    },
                    DireccionPartida = new Direccion
                    {
                        Ubigeo = "150101",
                        DireccionCompleta = "AV LIMA 455",
                        CodigoAnexo = "0000"
                    },
                    DireccionLlegada = new Direccion
                    {
                        Ubigeo = "160101",
                        DireccionCompleta = "AV ITALIA 555",
                        CodigoAnexo = "0000"
                    },
                    CodigoPuerto = string.Empty,
                    BienesATransportar = new List<DetalleGuia>()
                {
                    new DetalleGuia
                    {
                        Correlativo = 1,
                        CodigoItem = "0001",
                        Descripcion = "CHIP",
                        UnidadMedida = "NIU",
                        Cantidad = 4,
                        LineaReferencia = 1
                    }
                }
                };

                Console.WriteLine("Generando XML....");

                var documentoResponse = RestHelper<GuiaRemision, DocumentoResponse>.Execute("GenerarGuiaRemision", guia);

                if (!documentoResponse.Exito)
                {
                    throw new InvalidOperationException(documentoResponse.MensajeError);
                }

                Console.WriteLine("Firmando XML...");
                // Firmado del Documento.
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("certificado.pfx")),
                    PasswordCertificado = "nEu9YayAtTAyrMVcu",
                };

                var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

                if (!responseFirma.Exito)
                {
                    throw new InvalidOperationException(responseFirma.MensajeError);
                }

                File.WriteAllBytes("20610012036-09-" + guia.IdDocumento + ".xml", Convert.FromBase64String(responseFirma.TramaXmlFirmado));

                var json = JsonConvert.SerializeObject(guia, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                File.WriteAllText($"{guia.IdDocumento}.json", json);

                Console.WriteLine("Enviando a SUNAT....");

                var documentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = guia.Remitente.NroDocumento,
                    UsuarioSol = "DIMABER1",
                    ClaveSol = "GaTeUeGo1",
                    EndPointUrl = UrlSunatRest,
                    IdDocumento = guia.IdDocumento,
                    TipoDocumento = guia.TipoDocumento,
                    TramaXmlFirmado = responseFirma.TramaXmlFirmado,
                    Token = TokenRest
                };

                var enviarDocumentoResponse = RestHelper<EnviarDocumentoRequest, EnviarResumenResponse>.Execute("EnviarDocumentoRest", documentoRequest);

                if (!enviarDocumentoResponse.Exito)
                {
                    throw new InvalidOperationException(enviarDocumentoResponse.MensajeError);
                }

                //File.WriteAllBytes("GuaiRemisionCdr.zip", Convert.FromBase64String(enviarDocumentoResponse.TramaZipCdr));

                //Console.WriteLine("Respuesta de SUNAT:");
                //Console.WriteLine(enviarDocumentoResponse.MensajeRespuesta);

                Console.WriteLine("Respuesta de SUNAT Ticket:");
                Console.WriteLine(enviarDocumentoResponse.NroTicket);

                ConsultarTicketRest(enviarDocumentoResponse.NroTicket, documentoRequest.Ruc);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }

        }

        private static DocumentoResponse GenerarDocumento(DocumentoElectronico documento)
        {
            Console.WriteLine("Generando XML....");

            var metodo = "GenerarFactura";
            switch (documento.TipoDocumento)
            {
                case "01":
                case "03":
                    metodo = "GenerarFactura";
                    break;
                case "07":
                    metodo = "GenerarNotaCredito";
                    break;
                case "08":
                    metodo = "GenerarNotaDebito";
                    break;
            }

            var json = JsonConvert.SerializeObject(documento, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText($"{documento.IdDocumento}.json", json);

            var documentoResponse = RestHelper<DocumentoElectronico, DocumentoResponse>.Execute(metodo, documento);

            if (!documentoResponse.Exito)
            {
                throw new InvalidOperationException(documentoResponse.MensajeError);
            }

            return documentoResponse;
        }

        private static void FirmaryEnviar(DocumentoElectronico documento, DocumentoResponse documentoResponse)
        {
            Console.WriteLine("Firmando XML...");
            // Firmado del Documento.
            var firmado = new FirmadoRequest
            {
                TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("certificado.pfx")),
                PasswordCertificado = "nEu9YayAtTAyrMVcu",
                ValoresQr = documentoResponse.ValoresParaQr
            };

            var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

            if (!responseFirma.Exito)
            {
                throw new InvalidOperationException(responseFirma.MensajeError);
            }

            if (!string.IsNullOrEmpty(responseFirma.CodigoQr))
            {
                using (var mem = new MemoryStream(Convert.FromBase64String(responseFirma.CodigoQr)))
                {
                    Console.WriteLine("Guardando Imagen QR para el documento...");
                    var imagen = Image.FromStream(mem);
                    imagen.Save($"{documento.IdDocumento}.png");
                }
            }

            Console.WriteLine("Escribiendo el archivo {0}.xml .....", documento.IdDocumento);

            var path = $"{documento.IdDocumento}.xml";
            File.WriteAllBytes(path, Convert.FromBase64String(responseFirma.TramaXmlFirmado));

            Console.WriteLine("Enviando a SUNAT....");

            var documentoRequest = new EnviarDocumentoRequest
            {
                Ruc = documento.Emisor.NroDocumento,
                UsuarioSol = "MODDATOS",
                ClaveSol = "MODDATOS",
                EndPointUrl = UrlSunat,
                IdDocumento = documento.IdDocumento,
                TipoDocumento = documento.TipoDocumento,
                TramaXmlFirmado = responseFirma.TramaXmlFirmado
            };

            var enviarDocumentoResponse = RestHelper<EnviarDocumentoRequest, EnviarDocumentoResponse>.Execute("EnviarDocumento", documentoRequest);

            if (!enviarDocumentoResponse.Exito)
            {
                throw new InvalidOperationException(enviarDocumentoResponse.MensajeError);
            }

            File.WriteAllBytes($"{documento.IdDocumento}.zip", Convert.FromBase64String(enviarDocumentoResponse.TramaZipCdr));

            Console.WriteLine("Respuesta de SUNAT:");
            Console.WriteLine(enviarDocumentoResponse.MensajeRespuesta);
        }
    }
}
