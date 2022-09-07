using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListModel
{
    public class GetListModelQuery : IRequest<ModelListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, ModelListModel>
        {
            private readonly IModelRepository modelRepository;
            private readonly IMapper mapper;

            public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
            {
                this.modelRepository = modelRepository;
                this.mapper = mapper;
            }

            public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
            {
                                 //car models
                IPaginate<Model> models = await modelRepository.GetListAsync(include:
                                                       m => m.Include(c => c.Brand),
                                                       index: request.PageRequest.Page,
                                                       size: request.PageRequest.PageSize
                                                       );
                                //datamodel
                ModelListModel mappedModels = mapper.Map<ModelListModel>(models);
                return mappedModels;
            }
        }
    }
}
