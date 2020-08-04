using Sabio.Models.Domain;
using Sabio.Data;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Sabio.Data.Providers;
using Sabio.Models;

namespace Sabio.Services
{
    public class UserDashboardService : IUserDashboardService
    {
        Data.Providers.IDataProvider _data = null;

        public UserDashboardService(IDataProvider data)
        {
            _data = data;
        }

        public List<UserDashFavoriteVendors> GetTopFavoriteVendors()
        {
            List<UserDashFavoriteVendors> list = null;

            string procName = "dbo.UsersFavoriteVendors_SelectTop5ByRandom";

            _data.ExecuteCmd(procName, inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    UserDashFavoriteVendors aUserDashFavoriteVendor = MapUserDashFavoriteVendors(reader, out int index);

                    if (list == null)
                    {
                        list = new List<UserDashFavoriteVendors>();
                    }

                    list.Add(aUserDashFavoriteVendor);
                }
                );

            return list;
        }

        private static UserDashFavoriteVendors MapUserDashFavoriteVendors(IDataReader reader, out int startingIndex)
        {
            UserDashFavoriteVendors aUserDashFavoriteVendor = new UserDashFavoriteVendors();

            startingIndex = 0;

            aUserDashFavoriteVendor.Name = reader.GetSafeString(startingIndex++);
            aUserDashFavoriteVendor.Url = reader.GetSafeString(startingIndex++);
            aUserDashFavoriteVendor.Id = reader.GetSafeInt32(startingIndex++);
            aUserDashFavoriteVendor.UserId = reader.GetSafeInt32(startingIndex++);
            aUserDashFavoriteVendor.VendorId = reader.GetSafeInt32(startingIndex++);

            return aUserDashFavoriteVendor;
        }

        public List<UserDashShoppingCart> GetTopShoppingCartItems()
        {
            List<UserDashShoppingCart> list = null;

            string procName = "dbo.ShoppingCart_SelectTopFive";

            _data.ExecuteCmd(procName, inputParamMapper: null
            , singleRecordMapper: delegate (IDataReader reader, short set)
            {
                UserDashShoppingCart aUserDashShoppingCart = MapUserDashShoppingCart(reader, out int index);

                if (list == null)
                {
                    list = new List<UserDashShoppingCart>();
                }

                list.Add(aUserDashShoppingCart);
            }
            );

            return list;
        }

        private static UserDashShoppingCart MapUserDashShoppingCart(IDataReader reader, out int startingIndex)
        {
            UserDashShoppingCart aUserDashShoppingCart = new UserDashShoppingCart();

            startingIndex = 0;

            aUserDashShoppingCart.Id = reader.GetSafeInt32(startingIndex++);
            aUserDashShoppingCart.ProductId = reader.GetSafeInt32(startingIndex++);
            aUserDashShoppingCart.Quantity = reader.GetSafeInt32(startingIndex++);
            aUserDashShoppingCart.DateCreated = reader.GetSafeDateTime(startingIndex++);
            aUserDashShoppingCart.DateModified = reader.GetSafeDateTime(startingIndex++);
            aUserDashShoppingCart.CreatedBy = reader.GetSafeInt32(startingIndex++);

            return aUserDashShoppingCart;

        }

        public List<UserDashProduct> GetTopProducts()
        {
            List<UserDashProduct> list = null;

            string procName = "dbo.Products_SelectTopFiveByRandom";

            _data.ExecuteCmd(procName, inputParamMapper: null
            , singleRecordMapper: delegate (IDataReader reader, short set)
            {
                UserDashProduct aUserDashProduct = MapUserDashProduct(reader, out int index);

                if (list == null)
                {
                    list = new List<UserDashProduct>();
                }

                list.Add(aUserDashProduct);
            }
            );

            return list;
        }


        private static UserDashProduct MapUserDashProduct(IDataReader reader, out int startingIndex)
        {
            UserDashProduct aUserDashProduct = new UserDashProduct();

            startingIndex = 0;

            aUserDashProduct.Id = reader.GetSafeInt32(startingIndex++);
            aUserDashProduct.SKU = reader.GetSafeString(startingIndex++);
            aUserDashProduct.Name = reader.GetSafeString(startingIndex++);
            aUserDashProduct.IsVisible = reader.GetSafeBool(startingIndex++);
            aUserDashProduct.IsActive = reader.GetSafeBool(startingIndex++);
            aUserDashProduct.PrimaryImageId = reader.GetSafeInt32(startingIndex++);
            aUserDashProduct.Manufacturer = reader.GetSafeString(startingIndex++);
            aUserDashProduct.Year = reader.GetSafeInt32(startingIndex++);
            aUserDashProduct.Description = reader.GetSafeString(startingIndex++);
            aUserDashProduct.Specifications = reader.GetSafeString(startingIndex++);
            aUserDashProduct.DateCreated = reader.GetSafeDateTime(startingIndex++);
            aUserDashProduct.DateModified = reader.GetSafeDateTime(startingIndex++);
            aUserDashProduct.CreatedBy = reader.GetSafeInt32(startingIndex++);
            aUserDashProduct.ProductStatus = reader.GetSafeString(startingIndex++);
            aUserDashProduct.ProductTypeId = reader.GetSafeInt32(startingIndex++);
            aUserDashProduct.VendorId = reader.GetSafeInt32(startingIndex++);
            aUserDashProduct.BusinessId = reader.GetSafeInt32(startingIndex++);
            aUserDashProduct.VendorTypeId = reader.GetSafeInt32(startingIndex++);
            aUserDashProduct.VendorName = reader.GetSafeString(startingIndex++);
            aUserDashProduct.Url = reader.GetSafeString(startingIndex++);
            aUserDashProduct.ProductImages = reader.GetSafeString(startingIndex++);

            return aUserDashProduct;
        }


        public List<UserDashEvent> GetTopEvents()
        {
            List<UserDashEvent> list = null;

            string procName = "dbo.Events_SelectTopFiveByRandom";

            _data.ExecuteCmd(procName, inputParamMapper: null
            , singleRecordMapper: delegate (IDataReader reader, short set)
            {
                UserDashEvent aUserDashEvent = MapUserDashEvent(reader, out int index);

                if (list == null)
                {
                    list = new List<UserDashEvent>();
                }

                list.Add(aUserDashEvent);
                ;
            }
            );

            return list;
        }

        private static UserDashEvent MapUserDashEvent(IDataReader reader, out int startingIndex)
        {
            UserDashEvent aUserDashEvent = new UserDashEvent();

            startingIndex = 0;

            aUserDashEvent.Id = reader.GetSafeInt32(startingIndex++);
            aUserDashEvent.EventTypeId = reader.GetSafeInt32(startingIndex++);
            aUserDashEvent.CreatedBy = reader.GetSafeInt32(startingIndex++);
            aUserDashEvent.Name = reader.GetSafeString(startingIndex++);
            aUserDashEvent.Summary = reader.GetSafeString(startingIndex++);
            aUserDashEvent.ShortDescription = reader.GetSafeString(startingIndex++);
            aUserDashEvent.LocationId = reader.GetSafeInt32(startingIndex++);
            aUserDashEvent.EventStatusId = reader.GetSafeInt32(startingIndex++);
            aUserDashEvent.ImageUrl = reader.GetSafeString(startingIndex++);
            aUserDashEvent.ExternalSiteUrl = reader.GetSafeString(startingIndex++);
            aUserDashEvent.IsFree = reader.GetSafeBool(startingIndex++);
            aUserDashEvent.DateCreated = reader.GetSafeDateTime(startingIndex++);
            aUserDashEvent.DateModified = reader.GetSafeDateTime(startingIndex++);
            aUserDashEvent.DateStart = reader.GetSafeDateTime(startingIndex++);
            aUserDashEvent.DateEnd = reader.GetSafeDateTime(startingIndex++);

            return aUserDashEvent;

        }

    }
}
