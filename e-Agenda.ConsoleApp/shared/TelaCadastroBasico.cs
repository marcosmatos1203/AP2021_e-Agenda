using e_Agenda.Controladores;
using e_Agenda.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.shared
{
    public abstract class TelaCadastroBasico<T> : TelaBase where T : EntidadeBase
    {
        protected Controlador<T> controlador;
        public TelaCadastroBasico(string titulo, Controlador<T> controlador) : base(titulo)
        {
            this.controlador = controlador;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela(SubtituloDeInsercao());

            T registro = ObterRegistro(TipoAcao.Inserindo);

            string resultadoValidacao = controlador.AdicionarNovo(registro);

            if (resultadoValidacao == "ITEM_VALIDO")
                ApresentarMensagem(MensagemDeInsercaoComSucesso(), TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }
        public void ExcluirRegistro()
        {
            ConfigurarTela(SubtituloDeExclusao());

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\n" + PerguntaExclusaoQualRegistro());
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controlador.ExisteItem(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem(MensagemDeRegistroNaoEncontrado() + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }

            bool conseguiuExcluir = controlador.ExcluirItem(id);

            if (conseguiuExcluir)
                ApresentarMensagem(MensagemDeExclusaoComSucesso(), TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(MensagemDeExclusaoComErro(), TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }
        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela(SubtituloDeVisualizacao());

            List<T> registros = controlador.SelecionarTodos();

            if (registros.Count == 0)
            {
                ApresentarMensagem(MensagemDeListaVazia(), TipoMensagem.Atencao);
                return false;
            }
            ConfiguraTela(registros);

            return true;
        }

        public void EditarRegistro()
        {
            ConfigurarTela(SubtituloDeEdicao());

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\n" + PerguntaEdicaoQualRegistro());
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controlador.ExisteItem(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem(MensagemDeRegistroNaoEncontrado() + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            T registro = AtualizarRegistro(TipoAcao.Editando);

            string resultadoValidacao = controlador.Atualizar(id, registro);

            if (resultadoValidacao == "ITEM_VALIDO")
                ApresentarMensagem(MensagemDeEdicaoComSucesso(), TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public virtual string MensagemDeExclusaoComErro() { return "Falha ao tentar excluir o registro"; }
        public virtual string MensagemDeListaVazia() { return "Nenhum registro cadastrado!"; }
        public virtual string MensagemDeInsercaoComSucesso() { return "Registro inserido com sucesso"; }
        public virtual string MensagemDeEdicaoComSucesso() { return "Registro atualizado com sucesso"; }
        public virtual string MensagemDeRegistroNaoEncontrado() { return "Nenhum registro foi encontrado com este número: "; }
        public virtual string PerguntaEdicaoQualRegistro() { return "Digite o id do registro a ser editado: "; }
        public virtual string PerguntaExclusaoQualRegistro() { return "Digite o id do registro a ser excluído: "; }
        public virtual string SubtituloDeVisualizacao() { return "Visualizando registros.."; }
        public virtual string SubtituloDeEdicao() { return "Editando um registro"; }
        public virtual string SubtituloDeExclusao() { return "Excluindo um registro..."; }
        public virtual string SubtituloDeInsercao() { return "Inserindo um novo registro..."; }
        public virtual string MensagemDeExclusaoComSucesso() { return "Registro excluído com sucesso"; }
        public abstract T ObterRegistro(TipoAcao tipoAcao);
        public abstract T AtualizarRegistro(TipoAcao tipoAcao);
        protected abstract void ConfiguraTela(List<T> registros);
    }
}
