using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using appClinica.Core.Domain.Models;
using appClinica.Core.Application.Interfaces;
using appClinica.Core.Infraestructure.Repository.Abstract;

namespace appClinica.Core.Application.UseCases
{
    public class DiagnosticoUseCase : IDetailUseCase<Cita, Guid>
    {

        private readonly IBaseRepository<Cita, Guid> repository;
        private readonly IDetailRepository<Diagnostico, Guid> repositoryDiagnostico;

        public DiagnosticoUseCase(IBaseRepository<Cita, Guid> repository, IDetailRepository<Diagnostico, Guid> repositoryDiagnostico)
        {
            this.repository = repository;
            this.repositoryDiagnostico = repositoryDiagnostico;
        }

        public void Cancel(Guid entityId)
        {
            repositoryDiagnostico.Cancel(entityId);
            repository.SaveAllChanges();
        }

        public Cita Create(Cita entity)
        {
            var citaCreada = repository.Create(entity);
            citaCreada.Diagnosticos.ForEach(diagnostico => {
                repositoryDiagnostico.Create(diagnostico);
            });

            repository.SaveAllChanges();

            return citaCreada;
        }
    }
}
