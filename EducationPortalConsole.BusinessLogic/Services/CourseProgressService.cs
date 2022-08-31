using EducationPortalConsole.BusinessLogic.Comparers;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.Progress;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services;

public class CourseProgressService
{
    private readonly GenericRepository<CourseProgress> _courseProgressRepository;

    private readonly GenericRepository<Course> _courseRepository;

    public CourseProgressService()
    {
        _courseProgressRepository = new GenericRepository<CourseProgress>();
        _courseRepository = new GenericRepository<Course>();
    }

    public CourseProgressService(GenericRepository<CourseProgress> courseProgressRepository, GenericRepository<Course> courseRepository)
    {
        _courseProgressRepository = courseProgressRepository;
        _courseRepository = courseRepository;
    }

    public void SubscribeToCourse(User user, Course course)
    {
        if (user == null)
        {
            //todo logging???
            throw new ArgumentNullException(nameof(user));
        }

        if (course == null)
        {
            //todo logging?
            throw new ArgumentNullException(nameof(course));
        }

        if (CheckCourseSubscriptionExists(user, course))
        {
            //TODO what to do if subscription already exists?
            return;
        }

        var link = new CourseProgress
        {
            CourseId = course.Id,
            UserId = user.Id,
            Progress = 0
        };

        _courseProgressRepository.Add(link);
    }

    public bool CheckCourseSubscriptionExists(User user, Course course)
    {
        var existingLink = _courseProgressRepository.FindFirst(x => x.CourseId == course.Id && x.UserId == user.Id);

        return existingLink != null;
    }

    public void SetCourseProgress(User user, Course course, int progress)
    {
        var existingLink = _courseProgressRepository.FindFirst(x => x.CourseId == course.Id && x.UserId == user.Id);

        if (existingLink != null)
        {
            existingLink.Progress = progress;
            _courseProgressRepository.Update(existingLink);
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public int GetCourseProgress(User user, Course course)
    {
        var existingLink = _courseProgressRepository.FindFirst(x => x.CourseId == course.Id && x.UserId == user.Id);

        if (existingLink != null)
        {
            return existingLink.Progress;
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerable<Course> GetAvailableCourses(User user)
    {
        var all = _courseRepository.FindAll(
            _ => true,
            false,
            x => x.CourseProgresses
        ).ToList();

        var subbed = GetSubscribedCourses(user);

        return all.Except(subbed, new CourseComparer());
    }

    public IEnumerable<Course> GetSubscribedCourses(User user)
    {
        return _courseProgressRepository.FindAll(
            x => x.UserId == user.Id,
            false,
            x => x.Course)
            .Select(x => x.Course);
    }

    public IEnumerable<Course> GetInProgressCourses(User user)
    {
        return _courseProgressRepository.FindAll(
            x => x.UserId == user.Id && x.Progress < 100,
            false,
            x => x.Course)
            .Select(x => x.Course);
    }

    public IEnumerable<Course> GetFinishedCourses(User user)
    {
        return _courseProgressRepository.FindAll(
            x => x.UserId == user.Id && x.Progress >= 100,
            false,
            x => x.Course)
            .Select(x => x.Course);
    }
}