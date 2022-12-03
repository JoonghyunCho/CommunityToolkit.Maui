using Android.Content;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using AndroidUri = Android.Net.Uri;

namespace CommunityToolkit.Maui.Storage;

/// <inheritdoc />
public class FolderPickerImplementation : IFolderPicker
{
	const int requestCodeFolderPicker = 12345;

	/// <inheritdoc />
	public async Task<Folder> PickAsync(string initialPath, CancellationToken cancellationToken)
	{
		var status = await Permissions.RequestAsync<Permissions.StorageRead>();
		if (status is not PermissionStatus.Granted)
		{
			throw new PermissionException("Storage permission is not granted");
		}

		var intent = new Intent(Intent.ActionOpenDocumentTree);
		var pickerIntent = Intent.CreateChooser(intent, "Select folder") ?? throw new InvalidOperationException($"Unable to create intent.");

		Folder? folder = null;
		void OnResult(Intent intent)
		{
			var path = EnsurePhysicalPath(intent.Data);
			folder = new Folder(path, Path.GetFileName(path));
		}

		await IntermediateActivity.StartAsync(pickerIntent, requestCodeFolderPicker, onResult: OnResult);

		return folder ?? throw new FolderPickerException("Unable to get folder.");
	}

	/// <inheritdoc />
	public Task<Folder> PickAsync(CancellationToken cancellationToken)
	{
		return PickAsync(GetExternalDirectory(), cancellationToken);
	}

	static string GetExternalDirectory()
	{
		return Android.OS.Environment.ExternalStorageDirectory?.Path ?? "/storage/emulated/0";
	}

	static string EnsurePhysicalPath(AndroidUri? uri)
	{
		if (uri is null)
		{
			throw new FolderPickerException("Path is not selected.");
		}
		
		const string uriSchemeFolder = "content";
		if (uri.Scheme != null && uri.Scheme.Equals(uriSchemeFolder, StringComparison.OrdinalIgnoreCase))
		{
			var split = uri.Path? .Split(":") ?? throw new FolderPickerException("Unable to resolve path.");
			return Android.OS.Environment.ExternalStorageDirectory + "/" + split[1];
		}

		throw new FolderPickerException($"Unable to resolve absolute path or retrieve contents of URI '{uri}'.");
	}
}