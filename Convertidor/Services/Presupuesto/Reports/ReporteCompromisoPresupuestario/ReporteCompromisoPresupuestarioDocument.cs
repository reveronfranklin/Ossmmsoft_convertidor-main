﻿using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using Convertidor.Services.Presupuesto.ReporteCompromisoPresupuestario;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
using Microsoft.Data.SqlClient.DataClassification;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Convertidor.Services.Presupuesto.ReporteCompromisoPresupuestario
{
    public class ReporteCompromisoPresupuestarioDocument : IDocument
    {
        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        public ReporteCompromisoPresupuestarioDto Model;
        private readonly string _patchLogo;

        public ReporteCompromisoPresupuestarioDocument(ReporteCompromisoPresupuestarioDto model,
                                                  string patchLogo)
        {
            Model = model;
            _patchLogo = patchLogo;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            try
            {
                container
                    .Page(page =>
                    {
                        QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

                        page.Margin(10);
                        page.Size(PageSizes.A3);
                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);
                        page.Header().Element(ComposeHeader);

                        page.Footer().AlignCenter().Text(text =>
                        {

                            text.CurrentPageNumber();
                            text.Span(" / ");
                            text.TotalPages();
                        });

                        
                    });
            }
            catch (Exception ex) 
            {
              var message = ex.Message;   
            
            }
        }

        void ComposeHeader(IContainer container)
        {
            
        
            container.PaddingVertical(1).Column(column =>
            {
                
                
             
                column.Item().Row(row =>
                {
                    var encabezado = new EncabezadoComponent(Model.Encabezado);
                    row.ConstantItem(210).BorderLeft(1).BorderBottom(1).BorderTop(1).PaddingLeft(20).AlignLeft().AlignMiddle().ScaleToFit().Image(_patchLogo);
                    row.RelativeItem(4).BorderBottom(1).BorderTop(1).AlignCenter().AlignMiddle().PaddingRight(18).Text("COMPROMISO PRESUPUESTARIO").SemiBold().FontSize(14);
                    
                    row.RelativeItem().Column(col =>
                    {
   
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text(encabezado.ModelEncabezado.NumeroCompromiso).FontSize(12).Bold().FontColor(Colors.Red.Medium);
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("N° COMPROMISO").FontSize(8).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text(encabezado.ModelEncabezado.FechaCompromiso.ToShortDateString()).FontSize(8);
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("Fecha").FontSize(8).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(6).Text(encabezado.ModelEncabezado.NumeroSolicitud).FontSize(8).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("N° SOLICITUD").FontSize(8).SemiBold();
                    });
                
                    
                });


            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(5).Column(async column =>
            {

                column.Spacing(5);

                column.Item().PaddingTop(5).Row(row =>
                {
                    row.RelativeItem().Component(new EncabezadoComponent(Model.Encabezado));

                });



                column.Item().PaddingTop(5).Row(row =>
                {
                    row.RelativeItem().Component(new CuerpoComponent(Model.Cuerpo,Model.Encabezado));

                });

                column.Item().PageBreak();

                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new EncabezadoComponent(Model.Encabezado));

                });

            });
            
            
        }


        
    }


}

