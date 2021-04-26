using GardenPlannerData;
using GardenPlannerModels.AnswerModels;
using GardenPlannerModels.CommentModels;
using GardenPlannerModels.LikesModel;
using GardenPlannerModels.QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlannerServices
{
    public class SocialInteractionsService
    {
        private readonly ApplicationDbContext ctx = new ApplicationDbContext();
        private readonly Guid _userID;
        public SocialInteractionsService(Guid userID)
        {
            _userID = userID;
        }

        public bool AddComment(AddCommentModel model)
        {
            Comments comments = new Comments
            {
                PlantID = model.PlantID,
                Title = model.Title,
                Comment = model.Comment,
                UserID = _userID,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.Comments.Add(comments);
            return ctx.SaveChanges() == 1;
        }
        public bool AlterLikePlant(AlterLikeModel model)
        {
            bool isLiked = ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).IsLiked;
            if (isLiked)
            {
                ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).IsLiked = false;
            }
            else
            {
                ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).IsLiked = true;
            }
            return ctx.SaveChanges() == 1;
        }
        public bool AddQuestion(AddQuestionModel model)
        {
            Questions questions = new Questions
            {
                PlantID = model.PlantID,
                Question = model.Question,
                UserID = _userID,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.Questions.Add(questions);
            return ctx.SaveChanges() == 1;
        }
        public bool AddAnswer(AddAnswerModel model)
        {
            Answers answers = new Answers
            {
                QuestionID = model.QuestionID,
                Answer = model.Answer,
                UserID = _userID,
                CreatedDate = DateTimeOffset.UtcNow
            };
            ctx.Answers.Add(answers);
            return ctx.SaveChanges() == 1;
        }
        public IEnumerable<GetCommentsModel> GetComments(int plantID)
        {
            var query = ctx.Comments.Where(e => e.PlantID == plantID).Select(f => new GetCommentsModel
            {
                PlantID = f.PlantID,
                PlantName = ctx.Plants.Single(e => e.PlantID == f.PlantID).Name,
                Comments = ctx.Comments.Where(e => e.PlantID == f.PlantID).Select(g => new CommentModel
                {
                    Title = g.Title,
                    Comment = g.Comment,
                    Username = ctx.Users.Single(e => e.Id.ToString() == _userID.ToString()).UserName,
                    CreatedDate = DateTimeOffset.UtcNow
                }).ToList(),
                Likes = ctx.Likes.Where(e => e.PlantID == f.PlantID).Select(g => g.IsLiked==true).Count()
            });
            return query.ToList();
        }
        public int GetLikes(int plantID)
        {
            int likes = ctx.Likes.Where(e => e.PlantID == plantID).Select(g => g.IsLiked == true).Count();
            return likes;
        }
        public IEnumerable<GetQuestionsModel> GetQuestions(int plantID)
        {
            var query = ctx.Questions.Where(e => e.PlantID == plantID).Select(f => new GetQuestionsModel
            {
                QuestionID = f.QuestionID,
                Question = f.Question,
                CreatedDate = f.CreatedDate
            });
            return query.ToList();
        }
        public IEnumerable<GetQuestionAnswerModel> GetQuestionAnswers(int questionID)
        {
            var query = ctx.Answers.Where(e => e.QuestionID == questionID).Select(f => new GetQuestionAnswerModel
            {
                Question = ctx.Questions.Single(e => e.QuestionID == f.QuestionID).Question,
                CreatedDate = f.CreatedDate,
                Answers = ctx.Answers.Where(e => questionID == f.QuestionID).Select(g => new AnswerModel
                {
                    Answer = g.Answer,
                    CreatedDate = g.CreatedDate,
                    Username = ctx.Users.Single(e => e.Id.ToString() == _userID.ToString()).UserName
                }).ToList(),
            });
            return query.ToList();
        }
    }
}
