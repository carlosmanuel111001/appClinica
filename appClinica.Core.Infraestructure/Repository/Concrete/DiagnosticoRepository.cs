using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using appClinica.Core.Domain.Models;
using appClinica.Core.Infraestructure.Repository.Abstract;
using appClinica.Adapters.SQLServerDataAccess.Contexts;

namespace appClinica.Core.Infraestructure.Repository.Concrete
{
    public class DiagnosticoRepository : IDetailRepository<Diagnostico, Guid>
    {

        private ClinicaDB db;

        public DiagnosticoRepository(ClinicaDB db)
        {
            this.db = db;
        }

        public Diagnostico Create(Diagnostico entity)
        {
            throw new NotImplementedException();
        }

        public List<Diagnostico> GetDetailsByTransaction(Guid transactionId)
        {
            var diagnosticosSeleccionados = db.Diagnosticos
                .Where(diagnosticos => diagnosticos.citaId == transactionId)
                .ToList();

            return diagnosticosSeleccionados;
        }

        public void Cancel(Guid transactionId) {

            var diagnosticosSeleccionados = db.Diagnosticos
                .Where(diagnosticos => diagnosticos.citaId == transactionId)
                .ToList();

            if (diagnosticosSeleccionados.Any())
            {
                diagnosticosSeleccionados.ForEach(diagnostico =>
                {
                    db.Diagnosticos.Remove(diagnostico);
                });
            }
            else
            {
                throw new NullReferenceException("No se han encontrado diagnosticos");
            }
        }

        public void SaveAllChanges()
        {
            throw new NotImplementedException();
        }
    }
}
