using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StandWeb.Data;
using StandWeb.Models;


namespace StandWeb.Controllers
{

    public class CarrosController : Controller
    {
        /// <summary>
        /// atributo que representa a base de dados do projeto
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// atributo que contém os dados da app web no servidor
        /// </summary>
        private readonly IWebHostEnvironment _caminho;
        
        /// <summary>
        /// variavel que recolhe os dados da pessoa que se autenticou
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;


        public CarrosController(ApplicationDbContext context, IWebHostEnvironment caminho, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        // GET: Carros
        public async Task<IActionResult> Index()
        {


            return View(await _context.Carros.ToListAsync());
        }

        // GET: Carros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros
                .Where(f => f.IdCarros == id)
                .Include(f => f.ListaDeReviews)
                .ThenInclude(r => r.Utilizador)
                .OrderByDescending(f => f.Modelo)
                .Include(fc => fc.ListaDeMarcas)
                .FirstOrDefaultAsync(m => m.IdCarros == id);
            if (carro == null)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated) {
                //recolher dados do utilizador
                var utilizador = _context.Utilizadores.Where(u => u.UserNameId == _userManager.GetUserId(User)).FirstOrDefault();

                var gosto = await _context.Gostos.Where(f => f.CarrosFK == id && f.UtilizadoresFK == utilizador.IdUtilizador).FirstOrDefaultAsync();

                if (gosto == null) {
                    ViewBag.Gosto = false;
                } else {
                    ViewBag.Gosto = true;
                }

            }

            return View(carro);
        }



        /// <summary>
        /// Metodo para apresentar os comentarios feitos pelos utilizadores
        /// </summary>
        /// <param name="IdCarros"></param>
        /// <param name="comentario"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateComentario(int IdCarros, string comentario, int rating) {
            //recolher dados do utilizador
            var utilizador = _context.Utilizadores.Where(u => u.UserNameId == _userManager.GetUserId(User)).FirstOrDefault();

            if (utilizador.ControlarReview == false) {
                //variavel que contem os dados da review, do utilizador e sobre qual carro foi feita review
                var comment = new Reviews {
                CarrosFK = IdCarros,
                Comentario = comentario.Replace("\r\n", "<br />"),
                Pontuacao = rating,
                Data = DateTime.Now,
                Visibilidade = true,
                Utilizador = utilizador
            };
                //adiciona a review à Base de Dados
                _context.Reviews.Add(comment);
                //o utilizador já fez a sua review
                utilizador.ControlarReview = true;
                //guardar a alteração na Base de Dados
                _context.Utilizadores.Update(utilizador);
                //Guarda as alterações na Base de Dados
                await _context.SaveChangesAsync();
                //redirecionar para a página dos details do carro
                return RedirectToAction(nameof(Details),new { id = IdCarros});
            } else {
                return RedirectToAction(nameof(Details), new { id = IdCarros });
            }
            
        }

        public async Task<IActionResult> AdicionarGostos(int IdCarros) {
            //recolher dados do utilizador
            var utilizador = _context.Utilizadores.Where(u => u.UserNameId == _userManager.GetUserId(User)).FirstOrDefault();

            var gosto = await _context.Gostos.Where(f => f.CarrosFK == IdCarros && f.UtilizadoresFK == utilizador.IdUtilizador).FirstOrDefaultAsync();

            if (gosto == null) {
                //variavel que contem dados do utilizador e do carro
                var fav = new Gostos {
                    CarrosFK = IdCarros,
                    UtilizadoresFK = utilizador.IdUtilizador
                };
                //Adiciona o carro à Base de Dados
                _context.Gostos.Add(fav);
                //Guarda as alterações na Base de Dados
                await _context.SaveChangesAsync();
                //redirecionar para a página dos details do carro
                return RedirectToAction(nameof(Details), new { id = IdCarros });
            } else {
                //remove da base de dados 
                _context.Gostos.Remove(gosto);
                //Guarda as alterações na Base de Dados
                await _context.SaveChangesAsync();
                //redirecionar para a página dos details do carro
                return RedirectToAction(nameof(Details), new { id = IdCarros });
            }
        }

        // GET: Carros/Create
        public IActionResult Create()
        {
            ViewBag.ListaDeMarcas = _context.ListaDeMarcas.OrderBy(c => c.IdMarcas).ToList();
            return View();
        }
        
        // POST: Carros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarros,Modelo,Foto,Cilindrada,Potencia,Combustivel,Marca,Preco,Ano")] Carros carros,
            IFormFile imgFile, int[] MarcaEscolhida)
        {

            // avalia se o array com a lista de marcas escolhidas associadas ao carro está vazio ou não
            if (MarcaEscolhida.Length == 0) {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos uma marca.");
                // gerar a lista Marcas que podem ser associadas ao carro
                ViewBag.ListaDeMarcas = _context.ListaDeMarcas.OrderBy(c => c.IdMarcas).ToList();
                // devolver controlo à View
                return View(carros);
            }

            // criar uma lista com os objetos escolhidos das Marcas
            List<Marcas> listaDeMarcasEscolhidas = new List<Marcas>();
            // Para cada objeto escolhido..
            foreach (int item in MarcaEscolhida) {
                //procurar a marca
                Marcas Marca = _context.ListaDeMarcas.Find(item);
                // adicionar a marca à lista
                listaDeMarcasEscolhidas.Add(Marca);
            }

            // adicionar a lista ao objeto de "carro"
            carros.ListaDeMarcas = listaDeMarcasEscolhidas;

            carros.Foto = imgFile.FileName;

            //_webhost.WebRootPath vai ter o path para a pasta wwwroot
            var saveimg = Path.Combine(_caminho.WebRootPath, "fotos", imgFile.FileName);

            var imgext = Path.GetExtension(imgFile.FileName);

            if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG") {
                using (var uploadimg = new FileStream(saveimg, FileMode.Create)) {
                    await imgFile.CopyToAsync(uploadimg);

                }
            }

            if (ModelState.IsValid) {
                _context.Add(carros);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(carros);

        }
        
        // GET: Carros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //ViewBag.ListaDeMarcas = _context.ListaDeMarcas.OrderBy(c => c.IdMarcas).ToList();

            var carros = await _context.Carros
                                            .Include(l => l.ListaDeMarcas)
                                            .FirstOrDefaultAsync(m => m.IdCarros == id);


            if (carros == null)
            {
                return NotFound();
            }

            ViewBag.ListaDeMarcas = _context.ListaDeMarcas.OrderBy(c => c.Nome).ToList();

            return View(carros);
        }

        // POST: Carros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarros,Modelo,Foto,Cilindrada,Potencia,Combustivel,Marca,Preco,Ano")] Carros newCarros,
            IFormFile imgFile, int[] MarcaEscolhida)
        {
            

            if (id != newCarros.IdCarros)
            {
                return NotFound();
            }


            // dados anteriormente guardados do carro
            var carros = await _context.Carros
                                       .Where(l => l.IdCarros == id)
                                       .Include(l => l.ListaDeMarcas)
                                       .FirstOrDefaultAsync();

            // obter a lista dos IDs das Marcas associadas ao carro, antes da edição
            var oldListaMarcas = carros.ListaDeMarcas
                                           .Select(c => c.IdMarcas)
                                           .ToList();

            // avaliar se o utilizador alterou alguma Marcas associada ao carro
            // adicionadas -> lista de Marcas adicionadas
            // retiradas   -> lista de Marcas retiradas
            var adicionadas = MarcaEscolhida.Except(oldListaMarcas);
            var retiradas = oldListaMarcas.Except(MarcaEscolhida.ToList());

            // se alguma Marcas foi adicionada ou retirada
            // é necessário alterar a lista de Marcas 
            // associada à Lesson
            if (adicionadas.Any() || retiradas.Any())
            {

                if (retiradas.Any())
                {
                    // retirar a Marcas 
                    foreach (int oldMarca in retiradas)
                    {
                        var marcaToRemove = carros.ListaDeMarcas.FirstOrDefault(c => c.IdMarcas == oldMarca);
                        carros.ListaDeMarcas.Remove(marcaToRemove);
                    }
                }
                if (adicionadas.Any())
                {
                    // adicionar a Marcas 
                    foreach (int newMarca in adicionadas)
                    {
                        var marcaToAdd = await _context.ListaDeMarcas.FirstOrDefaultAsync(c => c.IdMarcas == newMarca);
                        carros.ListaDeMarcas.Add(marcaToAdd);
                    }
                }
            }

            // avalia se o array com a lista de Marcas escolhidas associadas ao carro está vazio ou não
            if (MarcaEscolhida.Length == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos uma marca.");
                // gerar a lista Marcas que podem ser associadas ao carro
                ViewBag.ListaDeMarcas = _context.ListaDeMarcas.OrderBy(c => c.IdMarcas).ToList();
                // devolver controlo à View
                return View(newCarros);
            }

            // avalia se o array com a lista de Marcas escolhidas associadas ao carro está vazio ou não
            if (MarcaEscolhida.Length < 1)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "Selecione apenas uma marca.");
                // gerar a lista Marcas que podem ser associadas ao carro
                ViewBag.ListaDeMarcas = _context.ListaDeMarcas.OrderBy(c => c.IdMarcas).ToList();
                // devolver controlo à View
                return View(newCarros);
            }

            // criar uma lista com os objetos escolhidos das Marcas
            List<Marcas> listaDeMarcasEscolhidas = new List<Marcas>();
            // Para cada objeto escolhido..
            foreach (int item in MarcaEscolhida)
            {
                //procurar a marca
                Marcas Marca = _context.ListaDeMarcas.Find(item);
                // adicionar a marca à lista
                listaDeMarcasEscolhidas.Add(Marca);
            }

            // adicionar a lista ao objeto de "Carro"
            newCarros.ListaDeMarcas = listaDeMarcasEscolhidas;


            /**************************************************/
            if (imgFile != null)
            {
                newCarros.Foto = imgFile.FileName;

                //_webhost.WebRootPath vai ter o path para a pasta wwwroot
                var saveimg = Path.Combine(_caminho.WebRootPath, "fotos", imgFile.FileName);

                var imgext = Path.GetExtension(imgFile.FileName);

                if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG")
                {
                    using (var uploadimg = new FileStream(saveimg, FileMode.Create))
                    {
                        await imgFile.CopyToAsync(uploadimg);

                    }
                }
            }
            else
            {
                Carros carros1 = _context.Carros.Find(newCarros.IdCarros);

                _context.Entry<Carros>(carros1).State = EntityState.Detached;


                newCarros.Foto = carros1.Foto;
            }

            /***************************************************/
            if (ModelState.IsValid)
            {
                try
                {
                    /* a EF só permite a manipulação de um único objeto de um mesmo tipo
                     *  por esse motivo, como estamos a usar o objeto 'carro'
                     *  temos de o atualizar com os dados que vêm da View
                     */

                    carros.Modelo = newCarros.Modelo;
                    carros.Cilindrada = newCarros.Cilindrada;
                    carros.Potencia = newCarros.Potencia;
                    carros.Combustivel = newCarros.Combustivel;
                    carros.Preco = newCarros.Preco;
                    carros.Ano = newCarros.Ano;
                    carros.Foto = newCarros.Foto;


                    // adição do objeto 'carro' para atualização
                    _context.Update(carros);
                    // 'commit' da atualização
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrosExists(carros.IdCarros))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // _context.Update(newCarros);
                //    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(newCarros);

        }

        // GET: Carros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carros = await _context.Carros
                .FirstOrDefaultAsync(m => m.IdCarros == id);
            if (carros == null)
            {
                return NotFound();
            }

            return View(carros);
        }

        // POST: Carros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carros = await _context.Carros.FindAsync(id);
            _context.Carros.Remove(carros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrosExists(int id)
        {
            return _context.Carros.Any(e => e.IdCarros == id);
        }
    }
}
