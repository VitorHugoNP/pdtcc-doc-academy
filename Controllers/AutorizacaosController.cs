using Microsoft.AspNetCore.Mvc;
using pdtcc_doc_academy.Models;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using pdtcc_doc_academy.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Org.BouncyCastle.Security;

namespace pdtcc_doc_academy.Controllers
{
    public class AutorizacaoController : Controller
    {
        private readonly AppDBContext _context; // Substitua pelo seu contexto do Entity Framework

        public AutorizacaoController(AppDBContext context)
        {
            _context = context;
        }

        // Ação para gerar e baixar o PDF
        
    }
}