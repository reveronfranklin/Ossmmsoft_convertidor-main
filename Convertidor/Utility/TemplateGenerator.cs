﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convertidor.Data.EntitiesDestino;

namespace AppService.Api.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLStringRecibo(List<HistoricoNomina> entity)
        {
         

            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                              <style>
                                .Subheader {
                                  text-align: center;
                                  color: green;
                                  padding-bottom: 10px;
                                }
                                .header {
                                  text-align: center;
                                  color: green;
                                  padding-bottom: 35px;
                                }

                                table {
                                  width: 80%;
                                  border-collapse: collapse;
                                }

                                td,
                                th {
                                  border: 1px solid gray;
                                  padding: 15px;
                                  font-size: 22px;
                                  text-align: center;
                                }

                                table th {
                                  background-color: green;
                                  color: white;
                                }
                              </style>
                            </head>
                            <body>
                                <div class='header'><h1>Recibo de Cobro</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Documento</th>
                                        <th>Cliente</th>
                                        <th>Vendedor</th>
                                        <th>Monto</th>
                                    </tr>");

            foreach (var item in entity)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", item.CODIGO_PERSONA, item.CODIGO_CONCEPTO, item.MONTO, item.TIPO);
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
