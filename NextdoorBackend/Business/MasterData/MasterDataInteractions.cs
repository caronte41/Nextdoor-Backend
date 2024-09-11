using Mapster;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.ClassLibrary.MasterData.Request;
using NextDoorBackend.ClassLibrary.MasterData.Response;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;

namespace NextDoorBackend.Business.MasterData
{
    public class MasterDataInteractions : IMasterDataInteractions
    {
        private readonly AppDbContext _context;

        public MasterDataInteractions(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetGenderByIdResponse> GetGenderById(GetGenderByIdRequest request)
        {
            var data = await _context.Genders
                .Where(f => f.Id == request.Id)
                .FirstOrDefaultAsync();
            var response = data.Adapt<GetGenderByIdResponse>();
            return response;
        }
        public async Task<GetBusinessCategoryByIdResponse> GetBusinessCategoryById(GetBusinessCategoryByIdRequest request)
        {
            var data = await _context.BusinessCategories
                .Where(f => f.Id == request.Id)
                .FirstOrDefaultAsync();
            var response = data.Adapt<GetBusinessCategoryByIdResponse>();
            return response;
        }
        public async Task<List<GetGendersResponse>> GetAllGenders(GetGendersRequest request)
        {
            var data = await _context.Genders.ToListAsync();
            var response = data.Adapt<List<GetGendersResponse>>();
            return response;
        }
        public async Task<List<GetBusinessCategoriesResponse>> GetAllBusinessCategories(GetBusinessCategoriesRequest request)
        {
            var data = await _context.BusinessCategories.ToListAsync();
            var response = data.Adapt<List<GetBusinessCategoriesResponse>>();
            return response;
        }
        public async Task<UpsertBusinessCategoryResponse> UpsertBusinessCategory(UpsertBusinessCategoriesRequest request)
        {
            var businessCategoryEntity = await _context.BusinessCategories.FirstOrDefaultAsync(e => e.Id == request.Id);

            if (businessCategoryEntity == null)
            {
                var newBusinessCategoryEntity  = new BusinessCategoriesEntity
                {
                    Id = Guid.NewGuid(),
                    CategoryName = request.CategoryName,
                    CategoryDescription = request.CategoryDescription,
                    CategorySubType=request.CategorySubType
                };

                _context.BusinessCategories.Add(newBusinessCategoryEntity);
                await _context.SaveChangesAsync();

                return new UpsertBusinessCategoryResponse
                {
                    Id = newBusinessCategoryEntity.Id
                };
            }
            else
            {
            
                businessCategoryEntity.CategoryName = request.CategoryName;
                businessCategoryEntity.CategoryDescription = request.CategoryDescription;
                businessCategoryEntity.CategorySubType = request.CategorySubType;

                _context.BusinessCategories.Update(businessCategoryEntity);
                await _context.SaveChangesAsync();

                return new UpsertBusinessCategoryResponse
                {
                    Id = businessCategoryEntity.Id
                };
            }
        }
        public async Task<UpsertGenderResponse> UpsertGender(UpsertGenderRequest request)
        {
            var genderEntity = await _context.Genders.FirstOrDefaultAsync(e => e.Id == request.Id);

            if (genderEntity == null)
            {
              
                var newGenderEntity = new GendersEntity
                {
                    Id = Guid.NewGuid(),
                    GenderName = request.GenderName
                   
                };

                _context.Genders.Add(newGenderEntity);
                await _context.SaveChangesAsync();

                return new UpsertGenderResponse
                {
                    Id = newGenderEntity.Id
                };
            }
            else
            {
               
                genderEntity.GenderName = request.GenderName;
             

                _context.Genders.Update(genderEntity);
                await _context.SaveChangesAsync();

                return new UpsertGenderResponse
                {
                    Id = genderEntity.Id
                };
            }
        }
        public async Task<string> SaveFile(byte[] fileData, string folderName)
        {
            var fileName = $"{Guid.NewGuid()}.jpg"; // Or .mp4 for videos
            var filePath = Path.Combine("wwwroot", folderName, fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            await Task.Run(() => File.WriteAllBytes(filePath, fileData));

            return $"/{folderName}/{fileName}";
        }


    }
}
