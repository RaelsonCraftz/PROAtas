# PROAtas
App for generating meeting minutes

This project aims for two things:

- Developing and maintaining an app to help people generate their meeting minutes without having to write a document on paper, then when getting into home/office finally type and format everything into a document. With the app, you type the topics and information in the meeting itself and generate the minute document as soon as the meeting is finished;
- A laboratory and playground to experiment with custom MVVM frameworks, reusing some concepts I've learned from the community and from other frameworks like Prism, MVVMCross and others, while still building some ideas of my own (like using Elements, which are basically ViewModels but built specifically for Entity types);

The major points about this project are as follows:

### Xamarin.Craftz

That's the project containing most of the MVVM framework, base services (like the LogService) and base implementations for BaseViewModel, BaseElement, BasePage, BaseDialog, etc.

### Stores

You can download the app at:

Play Store: https://play.google.com/store/apps/details?id=com.Raelson.PROAtas

App Store: TBD (v15 and the branch feature/ios is focusing on that)

### Story

This is one of the first apps that got me into mobile development. I've rebuilt it quite a few times and also rewritten it with different new ideas at some moments; for instance, the xamarin_old folder was one of such iterations where I've experimented the pure C# approach from https://github.com/VincentH-Net/CSharpForMarkup.
