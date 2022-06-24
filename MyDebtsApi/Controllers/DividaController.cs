using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDebtsApi.Data;
using MyDebtsApi.Extensions;
using MyDebtsApi.Models;
using MyDebtsApi.ViewModels;

namespace MyDebtsApi.Controllers{

    [ApiController]
    //[Route("v1")]
    public class DividaController : ControllerBase
    {

        [HttpGet("v1/dividas")]
        public async Task<IActionResult> Get(
        [FromServices]MyDebtsDbContext context)
        {
            try{
                var dividas = await context.Dividas.ToListAsync();
                return Ok(new ResultViewModel<List<DividaModel>>(dividas));
            }
            catch{
                return StatusCode(500, new ResultViewModel<List<DividaModel>>("01DI1 - Não foi possível buscar os dados no Servidor!"));
            }
            
            
        }

        [HttpGet("v1/dividas/{id:int}")]
        public async Task<IActionResult> Get(
        [FromRoute] int id,
        [FromServices] MyDebtsDbContext context)
        {
            try
            {
                var divida = await context.Dividas.FirstOrDefaultAsync(x=> x.Id == id);

                if(divida == null)
                {
                    return NotFound(new ResultViewModel<DividaModel>("01DI33 - Não foi localizado esta Dívida!"));
                }

                return Ok(new ResultViewModel<DividaModel>(divida));
            }
            catch(Exception ex){
                return StatusCode(500, new ResultViewModel<List<DividaModel>>("01DI3 - Não foi possível buscar os dados no Servidor!"));
            }
            
        }

        [HttpPost("v1/dividas")]
        public async Task<IActionResult> Post(
        [FromBody] EditorDividaViewModel model,
        [FromServices] MyDebtsDbContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<DividaModel>(ModelState.GetErros()));

            try
            {
                var divida = new DividaModel{
                    Id = 0,
                    Titulo = model.Titulo,
                    Descricao = model.Descricao

                };
                await context.Dividas.AddAsync(divida);
                await context.SaveChangesAsync();

                return Created($"v1/dividas/{divida.Id}", new ResultViewModel<DividaModel>(divida));
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<DividaModel>("01DI5 - Não foi possível inserir os dados no Servidor!"));
            }
            catch(Exception ex){
                return StatusCode(500, new ResultViewModel<DividaModel>("01DI6 - Falha interna do Servidor!"));
            }
            

            
        }

        [HttpPut("v1/dividas/{id:int}")]
        public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] EditorDividaViewModel model,
        [FromServices] MyDebtsDbContext context)
        {
            try{
                var divida = await context.Dividas.FirstOrDefaultAsync(x => x.Id == id);
                if (divida == null)
                {
                    return NotFound(new ResultViewModel<DividaModel>("Não foi localizado essa dívida!"));
                }

                divida.Titulo = model.Titulo;
                divida.Descricao = model.Descricao;
            
                context.Dividas.Update(divida);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<DividaModel>(divida));
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<DividaModel>("01DI7 - Não foi possível atualizar os dados no Servidor!"));
            }
            catch(Exception ex){
                return StatusCode(500, new ResultViewModel<DividaModel>("01DI8 - Falha interna do Servidor!"));
            }
            
        }

        [HttpDelete("v1/dividas/{id:int}")]
        public async Task<IActionResult> Delete(
        [FromRoute] int id,
        [FromServices] MyDebtsDbContext context)
        {
            try{
                var divida = await context.Dividas.FirstOrDefaultAsync(x => x.Id == id);
                if (divida == null)
                {
                    return NotFound(new ResultViewModel<DividaModel>("Dívida não encontrada!"));
                }

                context.Dividas.Remove(divida);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<DividaModel>(divida));
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<DividaModel>("01DI9 - Não foi possível excluir os dados no Servidor!"));
            }
            catch(Exception ex){
                return StatusCode(500, new ResultViewModel<DividaModel>("01DI10 - Falha interna do Servidor!"));
            }
            
            
        }


    }
    
}