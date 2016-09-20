===============================================================================

ParkSquare.Gracenote v1.0.2

(C) 2014 Park Square Consulting Ltd.

www.parksq.co.uk

===============================================================================


===========
Description
===========

This is a wrapper for the Gracenote Web API library to give out-of-the-box C# support for track, album, artist and cover artwork searching. 

The Gracenote Developer license is free for non-commercial use. 

Supports ALBUM_SEARCH and ALBUM_FETCH, with all options available that are exposed by the Gracenote Web API itself. 
For more information, see https://developer.gracenote.com/web-api#wrappers

=========================================
What is a Client ID and how do I get one?
=========================================

To begin development, the first thing you need to do is get a Client ID - a Gracenote API key that authorizes you to make calls to the 
Gracenote service. See https://developer.gracenote.com/en/frequently-asked-questions. Once you have a Client ID, calling the Web API 
using ParkSquare.Gracenote is really easy.

First, instantiate a new GracenoteClient object, passing in your API key:

            var client = new GracenoteClient("3509248-B9DB31219EF71DF036520E43918253A1");

You can now perform an album search, or an album fetch. 

===========
Album Fetch
===========

This is used when you already have the GracenoteId for an album and just want to retrieve it. Calls the Web API ALBUM_FETCH operation.

			// Fetch an album using a known Gracenote ID
            var x = client.Search("99337157-E1ED6101A3666CFD7D799529ED707E0F");

============
Album Search
============

This calls the Web API ALBUM_SEARCH operation, and can be used to perform searches of the Gracenote database.

It is possible to specify any combination of Album Title, Artist or Track Title. 

There are three search modes available:

	Default					Returns all matching albums
	BestMatch 				Returns a single album that best matches your search criteria
	BestMatchWithCover		As per BestMatch but also return the URL to an album art image, if available.


There are various other options that can include additional information about the album in the search results. Multiple flags can be specified.

Examples:

            var x = client.Search(new SearchCriteria
            {
                AlbumTitle = "Use Your Illusion",
                Artist = "Guns 'n' Roses",
                SearchMode = SearchMode.BestMatchWithCoverArt,
                SearchOptions = SearchOptions.Mood | SearchOptions.Tempo | SearchOptions.Cover | SearchOptions.ArtistImage
            });

            var y = client.Search(new SearchCriteria
            {
                Artist = "Pet Shop Boys",
                SearchMode = SearchMode.BestMatchWithCoverArt,
                SearchOptions = SearchOptions.Mood | SearchOptions.Tempo | SearchOptions.Cover | SearchOptions.ArtistImage
            });


================		
Artwork Download
================

If you have specified to return artwork information in the search response, and artwork is available for a particular album, 
then you will see a collection of Artwork objects available to you. These will generally contain URLs to a CDN containing
the image.

You can call the Download() method on any Artwork object to retrieve the image and save it to your local file system.


            var test = client.Search(new SearchCriteria
            {
                Artist = "Guns 'n' Roses",
                TrackTitle = "November Rain",
                SearchMode = SearchMode.BestMatchWithCoverArt,
                SearchOptions = SearchOptions.Mood | SearchOptions.Tempo | SearchOptions.Cover | SearchOptions.ArtistImage
            });

			test.Albums.First().Artwork.First().Download(@"C:\temp\AlbumArt.jpg");


======
Paging
======

Where a search yields many results, or it is desirable to page through the results, you can specify a start item and end item 
in your request. For example, this will return only the 10th to 14th results in the response:

            var k = client.Search(new SearchCriteria
            {
                Artist = "Guns 'n' Roses",
                Range = new Range(10, 14)
            });


The Count property on the SearchResult object will tell you how many items you can page through.
