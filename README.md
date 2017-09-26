Attempting to write a steam scraper that gets data for all of the apps available on the Steam library, and stores that data in a SQL database.
It then checks whether an app has changed from not free to free in the current scraping session, and if so tweets out the apps Steam url.

Part 1: Scraper - In progress
    Use the Steam API to gather all app data into a List object.
    Current issue - IP blocked after a certain amount of requests, block goes away after a seemingly random amount of time (2-4 minutes)
        solutions?
            After first 429 status code received, while loop and try again until it gets a successful hit
            Proxy IP switch - When 429 status received, switch IP address. Assuming it is being IP blocked, could be machine blocked as well..
                Not really the moral way to solve the problem, if this is the only solution probably abandon project..

Part 2: Data store
    Store the List of apps to a SQL database
        foreach list and perform an insert

Part 3: Tweet result
    Check the most recent entry for each app (as they're being entered) and tweet out the Steam URL if it has become free
        during the foreach loop in part 2, query database for the most recently added (by date column) entry with the same steam ID
        Check if status changed from "not free" to "free"
        If so, use Twitter API to tweet the link to the steam store