namespace STD.PL.Records;

public record StoryDetail(string By, int Descendants, int Id, int[] Kids, int Score, int Time, string Title, string Type, Uri Url);

public record Story(string By, int Descendants, int Id, int[] Kids, int Score, int Time, string Title, string Type, Uri Url);