using AttH1.Classes;
using Microsoft.AspNetCore.Mvc;


namespace AttH1.Controllers
{
    [ApiController]
    [Route("api/pessoa")]
    public class PessoaController : ControllerBase
    {
        public static List<Pessoa> pessoas = new List<Pessoa>();

        [HttpPost]

        public IActionResult AdicionarPessoa([FromBody] Pessoa pessoa)
        {
            pessoas.Add(pessoa);
            return Ok("Úsuario registrado.");
            return Created(string.Empty, pessoa);
        }

        [HttpDelete]
        public IActionResult PessoaRemover(string cpf)
        {
            var pessoaDeletar = pessoas.FirstOrDefault(pessoaDelete => pessoaDelete.Cpf == cpf);
            if (pessoaDeletar is null)
            {
                return NotFound();
            } else
            {
            pessoas.Remove(pessoaDeletar);
            return Ok("Usuário " + pessoaDeletar.Nome + "/CPF: " + pessoaDeletar.Cpf + " foi deletado com sucesso.");
            }
        }

        [HttpGet]
        [Route("BuscarTodasPessoas")]
        public IActionResult BuscarTodasPessoas()
        {
            return Ok(pessoas);
        }

        [HttpGet]
        [Route("{cpf}")]
        public IActionResult BuscarPessoaCPF([FromRoute] string cpf)
        {
            var pessoaBuscar = pessoas.FirstOrDefault(pessoaBusca => pessoaBusca.Cpf.Equals(cpf));
            return Ok(pessoaBuscar);
        }

        [HttpGet]
        [Route("{nome}")]
        public IActionResult BuscarPessoaEspecifica([FromRoute] string nome)
        {
            var pessoaBuscar = pessoas.Where(pessoaBusca => pessoaBusca.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(pessoaBuscar);
        }


        [HttpGet]
        [Route("PessoasIMC")]
        public IActionResult CalcularIMC()
        {
        
         var pessoasIMC = pessoas.Where(p => {double imc = CalcularIMC(p.Peso, p.Altura); return imc >= 18 && imc <= 24; });

            return Ok(pessoasIMC);
        }

        public static double CalcularIMC(double peso, double altura)
        {
            return peso / (altura * altura);
        } 

    }
}
