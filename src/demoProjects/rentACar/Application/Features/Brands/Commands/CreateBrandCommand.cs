using Application.Features.Brands.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands
{
    public class CreateBrandCommand : IRequest<CreateBrandDto>
    {
        public string Name { get; set; }

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreateBrandDto>
        {
            private readonly IBrandRepository brandRepository;
            private readonly IMapper mapper;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                this.brandRepository = brandRepository;
                this.mapper = mapper;
            }

            public async Task<CreateBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                Brand mappedBrand= mapper.Map<Brand>(request);
                Brand createdBrand = await brandRepository.AddAsync(mappedBrand);
                CreateBrandDto createdBrandDto = mapper.Map<CreateBrandDto>(createdBrand);
                return createdBrandDto;
            }
        }
    }
}
