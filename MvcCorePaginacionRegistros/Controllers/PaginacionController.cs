using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Models;
using MvcCorePaginacionRegistros.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaginacionRegistros.Controllers
{
    public class PaginacionController : Controller
    {
        private RepositoryHospital repo;

        public PaginacionController(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        public IActionResult PaginarGrupoVistaDepartamento(int? posicion)
        {
            if(posicion == null)
            {
                posicion = 1;
            }
            int numeroregistros = this.repo.GetNumeroRegistro();
            int numeroPagina = 1;
            string html = "<div>";
            for(int i = 1; i <= numeroregistros; i+=2)
            {
                html += "<a href='PaginarGrupoVistaDepartamento?posicion=" +
                    i + "'> Pagina " + numeroPagina + "</a> |";
                numeroPagina += 1;
            }
            html += "</div>";
            ViewData["LINKS"] = html;
            List<VistaDepartamento> lista = this.repo.getGrupoVistaDepartamento(posicion.Value);
            return View(lista);
        }

        public IActionResult PaginarRegistroVistaDepartamento(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numregistros = this.repo.GetNumeroRegistro();
            int siguiente = posicion.Value + 1;
            //DEBEMOS DE COMPROBAR SI NOS PASAMOS DEL NUMERO DE REGISTROS
            if (siguiente > numregistros)
            {
                //EFECTO OPTICO
                siguiente = numregistros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            VistaDepartamento vistaDep = this.repo.GetVistaDepartamento(posicion.Value);
            ViewData["ULTIMO"] = numregistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            return View(vistaDep);
        }
    }
}
