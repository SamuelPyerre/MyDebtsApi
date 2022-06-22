using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDebtsApi.Data;
using MyDebtsApi.Models;

namespace MyDebtsApi.Controllers{

    [ApiController]
    //Uma opção também
    //[Route("v1")]
    public class DividaController : ControllerBase
    {

        [HttpGet("v1/dividas")]
        public async Task<IActionResult> Get(
        [FromServices]MyDebtsDbContext context)
        {
            try{
                var dividas = await context.Dividas.ToListAsync();
                return Ok(dividas);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "01DI1 - Não foi possível buscar os dados no Servidor!");
            }
            catch(Exception ex){
                return StatusCode(500, "01DI2 - Falha interna do Servidor!");
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
                    return NotFound();
                }

                return Ok(divida);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "01DI3 - Não foi possível buscar os dados no Servidor!");
            }
            catch(Exception ex){
                return StatusCode(500, "01DI4 - Falha interna do Servidor!");
            }
            
        }

        [HttpPost("v1/dividas")]
        public async Task<IActionResult> Post(
        [FromBody] DividaModel model,
        [FromServices] MyDebtsDbContext context)
        {
            try
            {
                await context.Dividas.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/dividas/{model.Id}", model);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "01DI5 - Não foi possível inserir os dados no Servidor!");
            }
            catch(Exception ex){
                return StatusCode(500, "01DI6 - Falha interna do Servidor!");
            }
            

            
        }

        [HttpPut("v1/dividas/{id:int}")]
        public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] DividaModel model,
        [FromServices] MyDebtsDbContext context)
        {
            try{
                var divida = await context.Dividas.FirstOrDefaultAsync(x => x.Id == id);
                if (divida == null)
                {
                    return NotFound();
                }

                divida.Titulo = model.Titulo;
                divida.Descricao = model.Descricao;
            
                context.Dividas.Update(divida);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "01DI7 - Não foi possível atualizar os dados no Servidor!");
            }
            catch(Exception ex){
                return StatusCode(500, "01DI8 - Falha interna do Servidor!");
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
                    return NotFound();
                }

                context.Dividas.Remove(divida);
                await context.SaveChangesAsync();

                return Ok(divida);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "01DI9 - Não foi possível excluir os dados no Servidor!");
            }
            catch(Exception ex){
                return StatusCode(500, "01DI10 - Falha interna do Servidor!");
            }
            
            
        }


    }
    
}