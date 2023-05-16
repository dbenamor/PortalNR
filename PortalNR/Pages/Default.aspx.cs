using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;

namespace PortalNR.Pages
{
    public partial class Default : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IniciarPagina();
        }

        protected void btnLink1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Pages/ProximasTurmas.aspx");
        }

        protected void btnLink2_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Pages/NRaVencer.aspx");
        }

        protected void btnLink3_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Pages/NRExpirado.aspx");
        }

        private void IniciarPagina()
        {
            CarregarDadosModal();
            ConfigurarPerfilUsuario();
        }

        private void ConfigurarPerfilUsuario()
        {
            PerfilDAL perfil = new PerfilDAL();

            if (UsuarioLocalLogado == null) return;

            foreach (var item in perfil.ListaPerfil().Where(item => item.Id == UsuarioLocalLogado.PerfilRel.Id))
            {
                if (UsuarioLocalLogado != null && UsuarioLocalLogado.PerfilRel.Id != item.Id)
                    AlertaRedirecionar("Você não tem permissão de acessar esta página!", "../Login.aspx");
            }
        }

        private void CarregarDadosModal()
        {
            NormativosDAL nDAL = new NormativosDAL();

            var nr10 = nDAL.FindByNormativo("NR-10");
            lblNormativoNR10.Text = nr10.Normativo;
            if (nr10.Vigencia < 2)
                lblVigenciaNR10.Text = nr10.Vigencia.ToString() + " ano";
            else
                lblVigenciaNR10.Text = nr10.Vigencia.ToString() + " anos";
            lblDescricaoNR10.Text = nr10.Descricao;

            var nr10sep = nDAL.FindByNormativo("NR-10 SEP");
            lblNormativoNR10SEP.Text = nr10sep.Normativo;
            if (nr10sep.Vigencia < 2)
                lblVigenciaNR10SEP.Text = nr10sep.Vigencia.ToString() + " ano";
            else
                lblVigenciaNR10SEP.Text = nr10sep.Vigencia.ToString() + " anos";
            lblDescricaoNR10SEP.Text = nr10sep.Descricao;

            var nr11 = nDAL.FindByNormativo("NR-11");
            lblNormativoNR11.Text = nr11.Normativo;
            if (nr11.Vigencia < 2)
                lblVigenciaNR11.Text = nr11.Vigencia.ToString() + " ano";
            else
                lblVigenciaNR11.Text = nr11.Vigencia.ToString() + " anos";
            lblDescricaoNR11.Text = nr11.Descricao;

            var nr12 = nDAL.FindByNormativo("NR-12");
            lblNormativoNR12.Text = nr12.Normativo;
            if (nr12.Vigencia < 2)
                lblVigenciaNR12.Text = nr12.Vigencia.ToString() + " ano";
            else
                lblVigenciaNR12.Text = nr12.Vigencia.ToString() + " anos";
            lblDescricaoNR12.Text = nr12.Descricao;

            var nr33 = nDAL.FindByNormativo("NR-33");
            lblNormativoNR33.Text = nr33.Normativo;
            if (nr33.Vigencia < 2)
                lblVigenciaNR33.Text = nr33.Vigencia.ToString() + " ano";
            else
                lblVigenciaNR33.Text = nr33.Vigencia.ToString() + " anos";
            lblDescricaoNR33.Text = nr33.Descricao;

            var nr35 = nDAL.FindByNormativo("NR-35");
            lblNormativoNR35.Text = nr35.Normativo;
            if (nr35.Vigencia < 2)
                lblVigenciaNR35.Text = nr35.Vigencia.ToString() + " ano";
            else
                lblVigenciaNR35.Text = nr35.Vigencia.ToString() + " anos";
            lblDescricaoNR35.Text = nr35.Descricao;

            var reg55 = nDAL.FindByNormativo("REG-55");
            lblNormativoREG55.Text = reg55.Normativo;
            if (reg55.Vigencia < 2)
                lblVigenciaREG55.Text = reg55.Vigencia.ToString() + " ano";
            else
                lblVigenciaREG55.Text = reg55.Vigencia.ToString() + " anos";
            lblDescricaoREG55.Text = reg55.Descricao;
        }
    }
}