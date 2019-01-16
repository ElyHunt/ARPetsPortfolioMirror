# Dealing with Git and the Repository

These are just some short notes on Git and working with the repo. Right now it's just how to fetch and merge from, pull from, and push to both remotes for this repository, but tips and tricks for using Git, GitHub, and Azure may end up here as well.

## Adding a remote

Since we're using Azure DevOps for its project management, but still need to push changes to the GitHub repo sometimes, we'll need to add a remote location, instead of replacing the default. This isn't necessary; you can just push changes to the remote you have and someone who has it set up can pull and then push to the other remote.

### TLDR (full explainations left out anyway)

***Note:** This assumes you're on a local branch called `master` and that you're pushing and pulling the repo using `https`; if not, @kjmitch can elaborate on the options.*

You can set an additional remote branch for your local branch with the following command:

`git remote add <name> <location>`

This adds the URL of the repo in `<location>` as a remote branch called `<name>`. For example, to add a remote called `azure` for the Azure DevOps version of the repo, type the following:

`git remote add azure https://ARPets@dev.azure.com/ARPets/ARPets/_git/ARPets`

A note: You may need to supply the username and password you used with Azure DevOps to complete this part. Once that's done, you can check that the remote was added with the following command:

`git remote -v`

The listed result should look something like the following output:

``` bash
[kjmitch@ganymede sd-project-arpets]$ git remote -v
azure   git@ssh.dev.azure.com:v3/ARPets/ARPets/ARPets (fetch)
azure   git@ssh.dev.azure.com:v3/ARPets/ARPets/ARPets (push)
origin  git@github.com:cosc495x/sd-project-arpets.git (fetch)
origin  git@github.com:cosc495x/sd-project-arpets.git (push)
```

Once added, you can set your local branch to track the new remote instead of the default:

`git branch -u azure/master`

Check on which remote the branch is tracking with the following command:

`git branch -vva`

The two `v` switches are necessary to see all local branches and the remotes they are tracking. Including the `a` switch will list the remotes as well. The result listed will look something like the following output:

``` bash
[kjmitch@ganymede sd-project-arpets]$ git branch -vva
* master                5c91939 [azure/master] Have basic AI running
  remotes/azure/master  5c91939 Have basic AI running
  remotes/origin/master 5c91939 Have basic AI running
```

Another note: The local branch can only track one remote. You can use `push`, `pull`, and `fetch` with the other remote by specifying its name, or with both remotes by using the `--all` flag on these commands.
