using System;

class Program
{
    static void Main(string[] args)
   {
        // Create a list to store YouTube videos
        List<Video> videos = new List<Video>();

        // Create first video
        Video video1 = new Video("Introduction to C#", "Tech Guru", 600);
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks."));
        video1.AddComment(new Comment("Charlie", "Loved the clarity in the examples."));
        videos.Add(video1);

        // Create second video
        Video video2 = new Video("Machine Learning Basics", "AI Academy", 900);
        video2.AddComment(new Comment("David", "This was an amazing intro!"));
        video2.AddComment(new Comment("Ella", "More deep dives, please!"));
        video2.AddComment(new Comment("Frank", "Easy to follow. Thanks."));
        videos.Add(video2);

        // Create third video
        Video video3 = new Video("Web Development with React", "CodeMaster", 1200);
        video3.AddComment(new Comment("Grace", "React is amazing!"));
        video3.AddComment(new Comment("Hank", "Clear and concise."));
        video3.AddComment(new Comment("Ivy", "I learned so much, thank you."));
        videos.Add(video3);

        // Create fourth video
        Video video4 = new Video("Cybersecurity 101", "Security Hub", 750);
        video4.AddComment(new Comment("Jack", "This is an eye-opener."));
        video4.AddComment(new Comment("Karen", "Very informative."));
        video4.AddComment(new Comment("Leo", "What are some common vulnerabilities?"));
        videos.Add(video4);

        // Display all videos and their comments
        foreach (Video video in videos)
        {
            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine($" - {comment.CommenterName}: {comment.Text}");
            }
            Console.WriteLine("--------------------------------------\n");
        }
    }
}

// Class representing a comment
class Comment
{
    public string CommenterName { get; }
    public string Text { get; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

// Class representing a video
class Video
{
    public string Title { get; }
    public string Author { get; }
    public int Length { get; }
    public List<Comment> Comments { get; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    // Adds a comment to the video
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // Returns the total number of comments
    public int GetCommentCount()
    {
        return Comments.Count;
    }
}