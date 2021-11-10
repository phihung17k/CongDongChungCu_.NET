using CDCC.Bussiness.ViewModels.Apartment;
using CDCC.Data.CustomException;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Catalog.Apartments
{
    public class ApartmentService : IApartmentService
    {
        private IRepository<Apartment> repository;

        public ApartmentService(IRepository<Apartment> repository)
        {
            this.repository = repository;
        }

        public async Task<List<ApartmentViewModel>> GetAll()
        {
            List<ApartmentViewModel> apartmentViews = new List<ApartmentViewModel>();
            List<Apartment> apartments = await repository.Get_All();
            foreach (Apartment temp in apartments)
            {
                apartmentViews.Add(new ApartmentViewModel()
                {
                    Id = temp.Id,
                    Name = temp.Name,
                    Address = temp.Address,
                    Status = temp.Status
                });
            }
            return apartmentViews;
        }

        public async Task<ApartmentViewModel> Get(int id)
        {
            Apartment apartment = await repository.Get_By_Id(id);
            if (apartment == null)
            {
                throw new ApartmentIDNotFoundException("Not found apartment by this id");
            }
            return new ApartmentViewModel()
            {
                Id = apartment.Id,
                Name = apartment.Name,
                Address = apartment.Address,
                Status = apartment.Status
            };
        }

        public async Task<ApartmentViewModel> Insert(ApartmentInsertModel apartment)
        {
            Apartment temp = new Apartment()
            {
                Name = apartment.Name,
                Address = apartment.Address
            };
            int idApartment1 = await repository.Insert_(temp);
            ApartmentViewModel apartment1 = new ApartmentViewModel()
            {
                Id = idApartment1,
                Name = apartment.Name,
                Address = apartment.Address,
                Status = true
            };
            return apartment1;
        }

        public async Task<bool> Update(ApartmentViewModel apartment)
        {
            Apartment temp = await repository.Get_By_Id(apartment.Id);
            if (temp == null)
            {
                throw new ApartmentIDNotFoundException("The apartment is not exist");
            }
            temp.Name = apartment.Name;
            temp.Address = apartment.Address;
            temp.Status = apartment.Status;
            return await repository.Update_(temp);
        }

        public async Task<bool> UpdateStatus(int id)
        {
            Apartment temp = await repository.Get_By_Id(id);
            if (temp == null)
            {
                throw new ApartmentIDNotFoundException("The apartment is not exist");
            }
            if (temp.Status == false)
            {
                throw new ApartmentDeletedException("Deleted failed. The apartment was deleted");
            }
            temp.Name = temp.Name;
            temp.Address = temp.Address;
            temp.Status = false;
            return await repository.Update_(temp);
        }

        public async Task<bool> Delete(int id)
        {
            Apartment temp = await repository.Get_By_Id(id);
            if (temp == null)
            {
                throw new ApartmentIDNotFoundException("The apartment is not exist");
            }
            return await repository.Delete_(temp);
        }
    }
}
