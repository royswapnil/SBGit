using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;

namespace SterlingBankLMS.Core.Factories
{
    public class TrainingVideoFactory : GenericService<TrainingVideo>
    {
        public TrainingVideoFactory( IUnitOfWork unitOfWork ) : base(unitOfWork)
        {

        }

        public void DeleteTrainingVideo( TrainingVideo entity, int userID )
        {

            entity.IsDeleted = true;
            entity.LastModifiedById = userID;
            entity.ModifiedDate = AppHelper.GetCurrentDate();
            Update(entity);

            

        }

        public void SaveNewTrainingVideoUpload(TrainingVideoDto dto, int userId, int organId )
        {
            var video = new TrainingVideo();
            video.CreatedDate = DateTime.Now;
            video.IsDeleted = false;
            video.LastModifiedById = userId;
            video.ModifiedDate = DateTime.Now;
            video.OrganizationId = organId;
            video.TrainingVideoName = dto.TrainingVideoName;
            video.TrainingVideoUrl = dto.TrainingVideoUrl;
            video.CreatedById = userId;

            Add(video);
            

        }
    }
}
