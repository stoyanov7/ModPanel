namespace ModPanel
{
    using Models.ViewModels;
    using Utilities;

    public static class HtmlHelpers
    {
        public static string UsersToHtml(this AdminUsersViewModel u)
        {
            return $@"
                    <tr>
                        <td>{u.Id}</td>
                        <td>{u.Email}</td>
                        <td>{u.Position.PositionToFriendlyName()}</td>
                        <td>{u.Posts}</td>
                        <td>
                            {(u.IsApproved ? string.Empty : $@"<a class=""btn btn-primary btn-sm"" href=""/admin/approve?id={u.Id}"">Approve</a>")}
                        </td>
                    </tr>";
        }

        public static string PostsToHtml(this PostListingViewModel p)
        {
            return $@"
                    <tr>
                        <td>{p.Id}</td>
                        <td>{p.Title}</td>
                        <td>
                            <a class=""btn btn-warning btn-sm"" href=""/admin/edit?id={p.Id}"">Edit</a>
                            <a class=""btn btn-danger btn-sm"" href=""/admin/delete?id={p.Id}"">Delete</a>
                        </td>
                    </tr>";
        }

        public static string LogsToHtml(this AdminLogsViewModel log)
        {
            return $@"
                    <div class=""card border-{log.Type.ToViewClassName()} mb-1"">
                        <div class=""card-body"">
                            <p class=""card-text"">{log}</p>
                        </div>
                    </div>";
        }
    }
}