using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.ScheduleAvailability;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Sabio.Services
{
    public class ScheduleAvailabilityService : IScheduleAvailabilityService
    {
        IDataProvider _data = null;

        public ScheduleAvailabilityService(IDataProvider data) //Only things within this scope below can see "data"
        {
            _data = data; //Points to the IDataProvider called data
        }


        public Paged<ScheduleAvailability> GetByCreatedBy(int pageIndex, int pageSize, int createdBy)
        {
            Paged<ScheduleAvailability> pagedResult = null;
            List<ScheduleAvailability> result = null;
            int totalCount = 0;
            
    
                _data.ExecuteCmd(
                "dbo.ScheduleAvailability_ByCreatedBy",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PageIndex", pageIndex);
                    paramCollection.AddWithValue("@PageSize", pageSize);
                    paramCollection.AddWithValue("@CreatedBy", createdBy);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    ScheduleAvailability aScheduleAvailability = MapScheduleAvailability(reader, out int index);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index);
                    }

                    if (result == null)
                    {
                        result = new List<ScheduleAvailability>();
                    }

                    result.Add(aScheduleAvailability);
                }
            );

            if (result != null)
            {
                pagedResult = new Paged<ScheduleAvailability>(result, pageIndex, pageSize, totalCount);
            }

            return pagedResult;
        }

        public void Delete(int id)
        {
            string procname = "dbo.ScheduleAvailability_DeleteById";
            _data.ExecuteNonQuery(procname,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", id);
                },

                returnParameters: null);
        }

        public List<ScheduleAvailability> GetAll()
        {
            List<ScheduleAvailability> list = null;

            string procName = "dbo.ScheduleAvailability_SelectAll";

            _data.ExecuteCmd(procName, inputParamMapper: null
            , singleRecordMapper: delegate (IDataReader reader, short set)
            {
                ScheduleAvailability aScheduleAvailability = MapScheduleAvailability(reader, out int index);

                if (list == null)
                {
                    list = new List<ScheduleAvailability>();
                }

                list.Add(aScheduleAvailability);
            }
            );

            return list;
        }

        public ScheduleAvailability Get(int id)
        {
            string procName = "dbo.ScheduleAvailability_SelectById";

            ScheduleAvailability scheduleAvailability = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                scheduleAvailability = MapScheduleAvailability(reader, out int index);
            }
            );

            return scheduleAvailability;

        }

        public Paged<ScheduleAvailability> Paginate(int pageIndex, int pageSize)
        {
            Paged<ScheduleAvailability> pagedResult = null;
            List<ScheduleAvailability> result = null;
            ScheduleAvailability scheduleAvailability = null;

            int totalCount = 0;

            _data.ExecuteCmd(
                "dbo.ScheduleAvailability_SelectPaginated",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PageIndex", pageIndex);
                    paramCollection.AddWithValue("@PageSize", pageSize);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    scheduleAvailability = MapScheduleAvailability(reader, out int index);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index);
                    }

                    if (result == null)
                    {
                        result = new List<ScheduleAvailability>();
                    }

                    result.Add(scheduleAvailability);
                }
            );

            if (result != null)
            {
                pagedResult = new Paged<ScheduleAvailability>(result, pageIndex, pageSize, totalCount);
            }

            return pagedResult;

        }
        
        
        public void Update(ScheduleAvailabilityUpdateRequest model, int userId)
        {
            string procName = "dbo.ScheduleAvailability_Update";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@Id", model.Id);
                    col.AddWithValue("@ModifiedBy", userId);
                },

                returnParameters: null);
        }

        public int Add(ScheduleAvailabilityAddRequest model, int userId)
        {
            int id = 0;

            string procName = "dbo.ScheduleAvailabilty_Insert";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@CreatedBy", userId);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    col.Add(idOut);
                },

                returnParameters: delegate (SqlParameterCollection returnCollection)
                {

                    object oId = returnCollection["@Id"].Value;

                    int.TryParse(oId.ToString(), out id);
                }
            );

            return id;
        }

        private static ScheduleAvailability MapScheduleAvailability(IDataReader reader, out int startingIndex)
        {
            ScheduleAvailability aScheduleAvailability = new ScheduleAvailability();

            startingIndex = 0;

            aScheduleAvailability.Id = reader.GetSafeInt32(startingIndex++);
            aScheduleAvailability.ScheduleId = reader.GetSafeInt32(startingIndex++);
            aScheduleAvailability.StartTime = reader.GetSafeDateTime(startingIndex++);
            aScheduleAvailability.EndTime = reader.GetSafeDateTime(startingIndex++);
            aScheduleAvailability.DateCreated = reader.GetSafeDateTime(startingIndex++);
            aScheduleAvailability.DateModified = reader.GetSafeDateTime(startingIndex++);
            aScheduleAvailability.CreatedBy = reader.GetSafeInt32(startingIndex++);

            return aScheduleAvailability;
        }

        private static void AddCommonParams(ScheduleAvailabilityAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@ScheduleId", model.ScheduleId);
            col.AddWithValue("@StartTime", model.StartTime);
            col.AddWithValue("@EndTime", model.EndTime);
        }

    }
}
