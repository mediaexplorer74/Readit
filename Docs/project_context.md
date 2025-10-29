# Overview
- Readit: кроссплатформенное приложение на Xamarin.Forms (UWP, Android, iOS)
- Общая библиотека Readit (.NET Standard): Contracts, Models, Navigators, Presenters, Views
- Основные экраны: PostView (список постов), CommentView (детали поста и комментарии)

# Goals
- Обеспечить корректную навигацию из PostView в CommentView через MessagingCenter ("PostClicked") и передачу permalink
- Загружать и отображать заголовок/текст поста (t3) и список комментариев (t1) в CommentView
- Провести валидацию в UWP: проверка кликов по разным постам, отображение содержимого

# Progress
- Найдена подписка на MessagingCenter в PostView.xaml.cs для события "PostClicked" с вызовом _navigator.ShowCommentScreen(permalink)
- Подтверждена логика CommentPresenter.UpdateComments: запрос по permalink к Reddit API, десериализация в List<PostsModel>, обновление CommentView
- Обнаружено дублирование навигации: CommentNavigator.ShowCommentScreen также создаёт CommentView
- В CommentViewCell.Title очищается при привязке, что может вести к пустому заголовку в ячейке
- Нормализован permalink при отправке из PostViewCell (ведущий "/", без завершающего "/"), очищаются Content.GestureRecognizers перед добавлением жеста
- В PostView подписки MessagingCenter перенесены в OnAppearing с отпиской в OnDisappearing; удалена мутация строки TrimEnd('/'); добавлено диагностическое логирование полученного permalink
- В CommentPresenter нормализуется permalink перед формированием запроса; добавлено диагностическое логирование итогового URL

# Pending
- Настроить/валидировать Header в CommentView: заголовок/текст из корневого поста (t3), список комментариев (t1)
- Выполнить Restore и Build UWP (Debug x64) и проверить отображение комментариев, отсутствие двойной навигации
- Добавить/сохранить диагностическое логирование (PostView и CommentPresenter) для трассировки переходов и запросов