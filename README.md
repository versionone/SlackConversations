# SlackConversations

## Overview
SlackConversations is an example application that integrates VersionOne Conversations with Slack.

## What is VersionOne Conversations?
Conversations is a feature inside of VersionOne that allows members to communicate fluidly with and about each other as well as about assets in the system such as Stories. Similar to Twitter, you can mention other members and participate in conversations using 'reply' technology. But in addition to that you can mention stories and defects. When you mention a story, you can get email notifications when others particpate in the conversation.

## What is Slack?
Slack is a team communication tool that allows you to create topic-based rooms to chat in. We use Slack to communicate with our team since we are distributed and needed a tool to communicate with the whole team instantly. Slack has an API that allows you create integrations easily. For example we use the GitHub integration to post a message to a specific chat room when someone pushes.

## Why integrate the two?
We find that using Conversations is an extremely effective tool for creating and capturing communication about specific items, and specific people like a product manager and a tester and a developer. We are often referencing the expressions (a singular part of a conversations, analogous to a tweet) that may have happened weeks ago to get a better understanding of each others thoughts.

However easy it is to use, its not where our main team communication happens. And even though we get email notifications, many of us rarely check email. So if someone had a question, we were often doubling up our messages so that we capture the thought as well as get a prompt response. By integrating Conversations with Slack we can both get notified instantly where our main communication is happening, as well as preserve that communication in VersionOne that is easily referenced in the future.

## How to do
1. Clone it
1. Request an API key from Slack, and create a room
1. Create a user.config file that looks like this:
```
<appSettings>
	<add key="V1BaseUrl" value="https://your.hosted/instance" />
</appSettings>
```

1. Deploy the website to a location that can be reached by VersionOne
1. Create or edit your user.config in VersionOne and add that url:
```
<appSettings>
	<add key="ExpressionChangedWebHookUrl" value="http://your.server/SlackConversations/Expression/Changed" />
</appSettings>
```

1. Test it by visiting http://your.server/SlackConversations/Expression/Test

## Make it better
Got something you'd like to see in the application? Fork it and send us a pull request, we want it to be a great starting point for integrating with Conversations.