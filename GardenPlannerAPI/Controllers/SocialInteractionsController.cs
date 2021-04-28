using GardenPlannerModels.AnswerModels;
using GardenPlannerModels.CommentModels;
using GardenPlannerModels.LikesModel;
using GardenPlannerModels.QuestionModels;
using GardenPlannerServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GardenPlannerAPI.Controllers
{
    [Authorize]
    public class SocialInteractionsController : ApiController
    {
        private SocialInteractionsService CreateSocialInteractionsService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var plantService = new SocialInteractionsService(userID);
            return plantService;
        }
        [Route("api/AddComment")]
        public IHttpActionResult AddComment(AddCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateSocialInteractionsService();
            if (!service.AddComment(model))
            {
                return InternalServerError();
            }
            return Ok("Comment Added");
        }
        [Route("api/AlterLike")]
        public IHttpActionResult AlterLike(AlterLikeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateSocialInteractionsService();
            if (!service.AlterLikePlant(model))
            {
                return InternalServerError();
            }
            return Ok("Like Changed");
        }
        [Route("api/AddQuestion")]
        public IHttpActionResult AddQuestion(AddQuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateSocialInteractionsService();
            if (!service.AddQuestion(model))
            {
                return InternalServerError();
            }
            return Ok("Question Added");
        }
        [Route("api/AddAnswer")]
        public IHttpActionResult AddAnswer(AddAnswerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateSocialInteractionsService();
            if (!service.AddAnswer(model))
            {
                return InternalServerError();
            }
            return Ok("Answer Added");
        }
        [Route("api/GetComments")]
        public IHttpActionResult GetComments(int plantID)
        {
            var service = CreateSocialInteractionsService();
            var comments = service.GetComments(plantID);
            return Ok(comments);
        }
        [Route("api/GetLikes")]
        public IHttpActionResult GetLikes(int plantID)
        {
            var service = CreateSocialInteractionsService();
            var comments = service.GetLikes(plantID);
            return Ok(comments);
        }
        [Route("api/GetQuestions")]
        public IHttpActionResult GetQuestions(int plantID)
        {
            var service = CreateSocialInteractionsService();
            var questions = service.GetQuestions(plantID);
            return Ok(questions);
        }
        [Route("api/GetQuestionAnswers")]
        public IHttpActionResult GetQuestionAnswers(int questionID)
        {
            var service = CreateSocialInteractionsService();
            var questionAnswers = service.GetQuestionAnswers(questionID);
            return Ok(questionAnswers);
        }
    }
}
