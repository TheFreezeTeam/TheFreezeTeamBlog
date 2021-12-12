Title: Clean up your hard drive with WizTree
Published: 12/11/2021
Image: wiztreetreeviewwithmap.webp
Tags: 
  - dev-tools
Author: Steven T. Cramer
Description: How to see what is using up your hard drive.
Excerpt: To determine what was using up all the drive space, I needed better visibility. 
---

# X-Ray Vision to diagnose "Hard drive full"

I have a 2 TB drive, but early this week my "free space" was running low. I new I had created a couple of local Hyper-V VMs but thought, "I should have more free space than that!".

## Analysis (X-Ray Vision)

To determine what was using up all the drive space I needed better visibility. Luckily I found [WizTree](https://www.diskanalyzer.com/)

 ![wiztreetreeviewwithmap.webp](/images/wiztreetreeviewwithmap.webp)

A picture is worth a 1000 words. Very quickly WizTree showed me I had 250GB in my AMD log folder.

## Root Cause

At some point in the past, I guess, I had turned on logging in the Radeon Driver software and forgot to turn it off. So it just kept logging.

## Solution

Turn off the logging and then delete the logging folder.

I found a couple of other huge folders that were no longer needed.  In total, I cleaned up about 400GB off my drive.

Thanks WizTree.

## References
https://www.diskanalyzer.com/


>**Don't forget a good superhero keeps their code  clean and tested. And your hard drive too :)**
