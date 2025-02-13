using System.Collections.Generic;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            // artisyide genereerimine
            if (!context.Artists.Any())
            {

                var list1 = new Artist
                {
                    Name = "Artist 1"
                };
                var list2 = new Artist
                {
                    Name = "Artist 2"
                };
                var list3 = new Artist
                {
                    Name = "Artist 3"
                };
                var list4 = new Artist
                {
                    Name = "Artist 4"
                };
                var list5 = new Artist
                {
                    Name = "Artist 5"
                };
                var list6 = new Artist
                {
                    Name = "Artist 6"
                }; var list7 = new Artist
                {
                    Name = "Artist 7"
                }; var list8 = new Artist
                {
                    Name = "Artist 8"
                }; var list9 = new Artist
                {
                    Name = "Artist 9"
                }; var list10 = new Artist
                {
                    Name = "Artist 10"
                };
                // Add the list (which contains the artist) to the context
                context.Artists.Add(list1);
                context.Artists.Add(list2);
                context.Artists.Add(list3);
                context.Artists.Add(list4);
                context.Artists.Add(list5);
                context.Artists.Add(list6);
                context.Artists.Add(list7);
                context.Artists.Add(list8);
                context.Artists.Add(list9);
                context.Artists.Add(list10);
            }

            if (!context.ShowSchedule.Any())
            {
                var show1 = new ShowSchedule
                {
                    date = DateTime.Now
                };
                var show2 = new ShowSchedule
                {
                    date = DateTime.Now
                };
                var show3 = new ShowSchedule
                {
                    date = DateTime.Now
                };
                var show4 = new ShowSchedule
                {
                    date = DateTime.Now
                };
                var show5 = new ShowSchedule
                {
                    date = DateTime.Now
                };
                var show6 = new ShowSchedule
                {
                    date = DateTime.Now
                };
                var show7 = new ShowSchedule
                {
                    date = DateTime.Now
                };
                var show8 = new ShowSchedule
                {
                    date = DateTime.Now
                };
                var show9 = new ShowSchedule
                {
                    date = DateTime.Now
                };
                var show10 = new ShowSchedule
                {
                    date = DateTime.Now
                };

                context.ShowSchedule.Add(show1);
                context.ShowSchedule.Add(show2);
                context.ShowSchedule.Add(show3);
                context.ShowSchedule.Add(show4);
                context.ShowSchedule.Add(show5);
                context.ShowSchedule.Add(show6);
                context.ShowSchedule.Add(show7);
                context.ShowSchedule.Add(show8);
                context.ShowSchedule.Add(show9);
                context.ShowSchedule.Add(show10);

            }
            if (!context.MusicTracks.Any())
            {
                var track1 = new MusicTrack
                {
                    Title = "Title1",
                    Artist = "Name1",
                    Year = 1,
                    Pace = 1
                };
                var track2 = new MusicTrack
                {
                    Title = "Title2",
                    Artist = "Name2",
                    Year = 2,
                    Pace = 2
                };
                var track3 = new MusicTrack
                {
                    Title = "Title3",
                    Artist = "Name3",
                    Year = 3,
                    Pace = 3
                };
                var track4 = new MusicTrack
                {
                    Title = "Title4",
                    Artist = "Name4",
                    Year = 4,
                    Pace = 4
                };
                var track5 = new MusicTrack
                {
                    Title = "Title5",
                    Artist = "Name5",
                    Year = 5,
                    Pace = 5
                };
                var track6 = new MusicTrack
                {
                    Title = "Title6",
                    Artist = "Name6",
                    Year = 6,
                    Pace = 6
                };
                var track7 = new MusicTrack
                {
                    Title = "Title7",
                    Artist = "Name7",
                    Year = 7,
                    Pace = 7
                };
                var track8 = new MusicTrack
                {
                    Title = "Title8",
                    Artist = "Name8",
                    Year = 8,
                    Pace = 8
                };
                var track9 = new MusicTrack
                {
                    Title = "Title9",
                    Artist = "Name9",
                    Year = 9,
                    Pace = 9
                };
                var track10 = new MusicTrack
                {
                    Title = "Title10",
                    Artist = "Name10",
                    Year = 10,
                    Pace = 10
                };
                context.MusicTracks.Add(track1);
                context.MusicTracks.Add(track2);
                context.MusicTracks.Add(track3);
                context.MusicTracks.Add(track4);
                context.MusicTracks.Add(track5);
                context.MusicTracks.Add(track6);
                context.MusicTracks.Add(track7);
                context.MusicTracks.Add(track8);
                context.MusicTracks.Add(track9);
                context.MusicTracks.Add(track10);
            }
            // Save changes to the database
            context.SaveChanges();

        }
    }
}