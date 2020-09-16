# HTL Leonding - C# Course Material

## Content

This repository contains the material that I use during my C# course at [HTL Leonding](https://www.htl-leonding.at/).

## View Slides

### Locally

You need to have Node.js installed in order to view slides locally. Run `npm install` to install necessary packages.

To view a certain chapter, run `npm start -- ./slides/0010-course-overview.md` (replace *0010-course-overview.md* with the name of the chapter you want to view).

If you would like to view all the slides, run `npm build`. It will create *slides/9999_full.md*. You can view this file just like a single chapter (see description above).

### Docker

The slides are available as a Docker image [on the Docker Hub](https://hub.docker.com/repository/docker/rstropek/htl-leo-csharp-4-slides/general) (*rstropek/htl-leo-csharp-4-slides*). To view slides locally on e.g. port 8081 run `docker run -p 8081:80 rstropek/htl-leo-csharp-4-slides`.
