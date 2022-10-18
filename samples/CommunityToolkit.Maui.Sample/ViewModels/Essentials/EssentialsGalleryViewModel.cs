﻿using CommunityToolkit.Maui.Sample.Models;

namespace CommunityToolkit.Maui.Sample.ViewModels.Essentials;

public class EssentialsGalleryViewModel : BaseGalleryViewModel
{
	public EssentialsGalleryViewModel()
		: base(new[]
		{
			SectionModel.Create<FolderPickerViewModel>("FolderPicker", "Allows pick folder from local file storage"),
			SectionModel.Create<SaveFileDialogViewModel>("SaveFileDialog", "Allows save file to local file storage")
		})
	{
	}
}