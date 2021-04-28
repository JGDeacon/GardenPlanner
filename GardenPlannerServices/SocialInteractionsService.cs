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
            if (ctx.Likes.Where(e => e.PlantID == model.PlantID && e.UserID == _userID).Count() < 1) 
            {
                Likes likes = new Likes
                {
                    IsLiked = true,
                    PlantID = model.PlantID,
                    UserID = _userID,
                    CreatedDate = DateTimeOffset.UtcNow,
                };
                ctx.Likes.Add(likes);
            }
            else
            {
                bool isLiked = ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).IsLiked;
                if (isLiked)
                {
                    ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).IsLiked = false;
                    ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).ModifiedDate = DateTimeOffset.UtcNow;
                }
                else
                {
                    ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).IsLiked = true;
                    ctx.Likes.Single(e => e.PlantID == model.PlantID && e.UserID == _userID).ModifiedDate = DateTimeOffset.UtcNow;
                }
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
        public GetCommentsModel GetComments(int plantID) 
        {
            Plants plants = ctx.Plants.Single(e => e.PlantID == plantID);
            GetCommentsModel query =  new GetCommentsModel
            {
                PlantID = plants.PlantID,
                PlantName = plants.Name,
                Comments = ctx.Comments.Where(e => e.PlantID == plants.PlantID).Select(g => new CommentModel
                {
                    Title = g.Title,
                    Comment = g.Comment,
                    Username = ctx.Users.FirstOrDefault(e => e.Id.ToString() == g.UserID.ToString()).UserName,
                    CreatedDate = g.CreatedDate
                }).ToList(),
                Likes = ctx.Likes.Where(e => e.PlantID == plants.PlantID).Select(g => g.IsLiked == true).Count()
            };
            return query;
        }
        public int GetLikes(int plantID)
        {
            int count = 0;
            List<Likes> likes = ctx.Likes.Where(e => e.PlantID == plantID).ToList();
            foreach (Likes item in likes)
            {
                if (item.IsLiked)
                {
                    count++;
                }
            }
            return count;
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
        public GetQuestionAnswerModel GetQuestionAnswers(int questionID) 
        {
            Questions questions = ctx.Questions.Single(e => e.QuestionID == questionID);
            GetQuestionAnswerModel getQuestionAnswerModel = new GetQuestionAnswerModel
            {
                Question = questions.Question,
                CreatedDate = questions.CreatedDate,
                Answers = ctx.Answers.Where(e => e.QuestionID == questions.QuestionID).Select(g => new AnswerModel
                {
                    Answer = g.Answer,
                    CreatedDate = g.CreatedDate,
                    Username = ctx.Users.FirstOrDefault(e => e.Id.ToString() == g.UserID.ToString()).UserName
                }).ToList(),
            };
            return getQuestionAnswerModel;
        }
    }
}
