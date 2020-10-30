﻿using System;
using System.Net;
using System.Reflection;
using System.Text;

namespace ByteBank.Portal.Infraestrutura
{
    public class WebApplication
    {
        private readonly string[] _prefixos;

        public WebApplication(string[] prefixos)
        {
            if (prefixos == null)
                throw new ArgumentNullException(nameof(prefixos));

            _prefixos = prefixos;
        }

        public void Iniciar()
        {
            while (true)
            {
                ManipularRequisicao();
            }
        }

        private void ManipularRequisicao()
        {
            var httpListener = new HttpListener();

            foreach (var prefixo in _prefixos)
            {
                httpListener.Prefixes.Add(prefixo);
            }

            httpListener.Start();

            var contexto = httpListener.GetContext();
            var requisicao = contexto.Request;
            var resposta = contexto.Response;

            var path = requisicao.Url.AbsolutePath;

            var assembly = Assembly.GetExecutingAssembly();
            var nomeResource = Utilidades.ConverterPathParaNomeAssembly(path);

            var resourceStream = assembly.GetManifestResourceStream(nomeResource);
            var bytesResource = new byte[resourceStream.Length];

            resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);

            resposta.ContentType = Utilidades.ObterTipoDeConteudo(path);
            resposta.StatusCode = (int)HttpStatusCode.OK;
            resposta.ContentLength64 = resourceStream.Length;

            resposta.OutputStream.Write(bytesResource, 0, bytesResource.Length);

            resposta.OutputStream.Close();

            httpListener.Stop();
        }
    }
}
