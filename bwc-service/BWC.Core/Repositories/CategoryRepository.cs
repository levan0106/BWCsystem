using BWC.Core.Interfaces;
using BWC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BWC.Core.Repositories.MsSql
{
    public class CategoryRepository : BaseRepository, ICategory
    {
        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Category>(@"
                      call  sp_GetAllCategory
                    ", new { }).ToList();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get All Category: ", e);
                throw;
            }
        }

        public Category GetInfo(object id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.Query<Category>(@"
                      call  sp_GetCategory(@id)
                    ", new { id }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                BWC.Core.Common.LogManager.LogError("Get Category Ìno: ", e);
                throw;
            }
        }

        public bool Insert(Category entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Category entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    call sp_UpdateCategory (
                                    @Id,
                                    @CategoryCode,
                                    @CategoryName,
                                    @Description,
                                    @TubeWidth,
                                    @TubeTypewidthLess,
                                    @TubeTypeWidthGreater,
                                    @BottomRailWidth,
                                    @MaterialWidth,
                                    @MaterialDrop,
                                    @HoodEqualWidth,
                                    @FrameWidth,
                                    @FrameDrop,
                                    @MeshWidth,
                                    @MeshDrop,
                                    @TubeType,
                                    @OuterTrackDrop,
                                    @InnerTrackDrop,
                                    @MaterialKederWidth,
                                    @BoxTypeDrop,
                                    @BoxLength,
                                    @BottomBarHeight,
                                    @BottomBarLength,
                                    @SlatHeight,
                                    @SlatAmount,
                                    @SlatLenght,
                                    @AxleLenght,
                                    @GuideLenght,
                                    @ActiveStatus
                            )
                    ", entity);
                    return true;
                }
                catch (Exception e)
                {
                    BWC.Core.Common.LogManager.LogError("Update Category: ", e);
                    return false;
                }

            }
        }
    }
}
