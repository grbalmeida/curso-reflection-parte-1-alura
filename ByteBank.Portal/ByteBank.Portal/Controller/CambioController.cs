﻿using ByteBank.Portal.Infraestrutura;
using ByteBank.Service;
using ByteBank.Service.Cambio;
using System.IO;
using System.Reflection;

namespace ByteBank.Portal.Controller
{
    public class CambioController : ControllerBase
    {
        private ICambioService _cambioService;

        public CambioController()
        {
            _cambioService = new CambioTesteService();
        }

        public string MXN()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);

            var textoPagina = View();

            var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());

            return textoResultado;
        }

        public string USD()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);

            var textoPagina = View();

            var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());

            return textoResultado;
        }

        public string Calculo(string moedaOrigem, string moedaDestino, decimal valor)
        {
            var valorFinal = _cambioService.Calcular(moedaOrigem, moedaDestino, valor);

            var textoPagina = View();

            var textoResultado =
                textoPagina
                    .Replace("VALOR_MOEDA_ORIGEM", valor.ToString())
                    .Replace("VALOR_MOEDA_DESTINO", valorFinal.ToString())
                    .Replace("MOEDA_ORIGEM", moedaOrigem)
                    .Replace("MOEDA_DESTINO", moedaDestino);

            return textoResultado;
        }
    }
}
