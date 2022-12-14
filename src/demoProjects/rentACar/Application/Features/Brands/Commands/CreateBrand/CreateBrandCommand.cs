using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
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
            private readonly BrandBusinessRules brandBusinessRules;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                this.brandRepository = brandRepository;
                this.mapper = mapper;
                this.brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreateBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

                Brand mappedBrand= mapper.Map<Brand>(request);
                Brand createdBrand = await brandRepository.AddAsync(mappedBrand);
                CreateBrandDto createdBrandDto = mapper.Map<CreateBrandDto>(createdBrand);
                return createdBrandDto;
            }
        }
    }
}
