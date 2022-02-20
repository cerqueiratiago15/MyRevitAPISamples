using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlPoints
{
    public class ViewModel: Notifier
    {

		#region Collections


		private ObservableCollection<Family> families;

		public ObservableCollection<Family> Families
		{
			get { return families; }
			set
			{
				if (value != families)
				{
					families = value;
				}
				this.OnPropertyChanged(nameof(Families));
			}
		}

		private ObservableCollection<FamilySymbol> familyTypes;

		public ObservableCollection<FamilySymbol> FamilyTypes
		{
			get { return familyTypes; }
			set
			{
				if (familyTypes != value)
				{
					familyTypes = value;
				}
				this.OnPropertyChanged(nameof(FamilyTypes));
			}
		}

		private ObservableCollection<MyParameters> parameters;

		public ObservableCollection<MyParameters> Parameters
		{
			get { return parameters; }
			set
			{
				if (value != parameters)
				{
					parameters = value;
				}
				this.OnPropertyChanged(nameof(Parameters));
			}
		}

		private ObservableCollection<MyParameters> selectedParameters;

		public ObservableCollection<MyParameters> SelectedParameters
		{
			get { return selectedParameters; }
			set
			{
				if (value != selectedParameters)
				{
					selectedParameters = value;
				}
				this.OnPropertyChanged(nameof(SelectedParameters));
			}
		}

		private ObservableCollection<MyParameters> addParameters;

		public ObservableCollection<MyParameters> AddParameters
		{
			get { return addParameters; }
			set
			{
				if (value != addParameters)
				{
					addParameters = value;
				}
				this.OnPropertyChanged(nameof(AddParameters));
			}
		}

		private ObservableCollection<MyParameters> removeParameter;

		public ObservableCollection<MyParameters> RemoveParameter
		{
			get { return removeParameter; }
			set
			{
				if (removeParameter !=value)
				{
					removeParameter = value;
				}
				this.OnPropertyChanged(nameof(RemoveParameter));
			}
		}

		private ObservableCollection<MyParameters> selectedRemove;

		public ObservableCollection<MyParameters> SelectedRemove
		{
			get { return selectedRemove; }

			set
			{
				if (this.selectedRemove != value)
				{
					this.selectedRemove = value;
					
				}
				this.OnPropertyChanged(nameof(SelectedRemove));
			}
		}


		#endregion

		#region Properties

		private Family selectedFamily;

		public Family SelectedFamily
		{
			get { return selectedFamily; }
			set
			{
				if (value!=selectedFamily)
				{
					selectedFamily = value;
				}
				this.OnPropertyChanged(nameof(SelectedFamily));
				GetTypes();
			}
		}

		private FamilySymbol selectedType;

		public FamilySymbol SelectedType
		{
			get { return selectedType; }
			set
			{
				if (value!= selectedType)
				{
					selectedType = value;
				}
				this.OnPropertyChanged(nameof(SelectedType));
				GetParameters();
			}
		}

		private ApplicationPage currentPage;

		public ApplicationPage CurrentPage
		{
			get { return currentPage; }

			set
			{
				if (value != currentPage)
				{
					currentPage = value;
				}
				this.OnPropertyChanged(nameof(CurrentPage));
			}
	}

		private Document Doc { get; set; }

		private bool exportCurrentView;

		public bool ExportCurrentView
		{
			get { return exportCurrentView; }
			set
			{
				if (value!=exportCurrentView)
				{
					exportCurrentView = value;
				}
				this.OnPropertyChanged(nameof(this.ExportCurrentView));
			}
		}

		private bool exportEntireProject;

		private bool zCoordinate;

		public bool ZCoordinate
		{
			get { return zCoordinate; }
			set
			{
				if (value!= zCoordinate)
				{
					zCoordinate = value;
				}
				this.OnPropertyChanged(nameof(ZCoordinate));
			}
		}

		private bool hideInView;

		public bool HideInView
		{
			get { return hideInView; }
			set
			{
				if (value!= hideInView)
				{
					hideInView = value;
				}
				this.OnPropertyChanged(nameof(HideInView));
			}
		}


		public bool ExportEntireProject
		{
			get { return exportEntireProject; }
			set
			{
				if (value != exportEntireProject)
				{
					exportEntireProject = value;
				}
				this.OnPropertyChanged(nameof(this.ExportEntireProject));
			}
		}

		#endregion

		#region Commands

		public ICommand GetTypesCommand { get; private set; }

		public ICommand AddParametersCommand { get; set; }

		public ICommand RemoveParametersCommand { get; set; }

		public ICommand CallParameterPageCommand { get; set; }

		public ICommand OkCommand { get; set; }

		public ICommand ExportCommand { get; set; }

		public ICommand MoveUpCommand { get; set; }

		public ICommand MoveDownCommand { get; set; }

		#endregion

		#region Constructor

		public ViewModel(Document doc)
		{
			this.ExportCurrentView = true;

			this.GetTypesCommand = new RelayCommand(GetTypes);

			this.AddParametersCommand = new RelayCommand(AddParametersM);

			this.RemoveParametersCommand = new RelayCommand(RemoveParameters);

			this.CallParameterPageCommand = new RelayCommand(CallParameterPage);

			this.OkCommand = new RelayCommand(Ok);

			this.ExportCommand = new RelayCommand(Export);

			this.addParameters = new ObservableCollection<MyParameters>();

			this.MoveUpCommand = new RelayCommand(MoveUp);

			this.MoveDownCommand = new RelayCommand(MoveDown);

			this.CurrentPage = ApplicationPage.Main;

			PageConverter.model = this;

			this.HideInView = false;

			this.Doc = doc;
		}

		#endregion

		#region Methods

		private void GetTypes()
		{
			this.FamilyTypes = null;

			if (this.SelectedFamily != null)
			{
				this.FamilyTypes = new ObservableCollection<FamilySymbol>(Utils.GetFamilyTypes(this.Doc, this.SelectedFamily));
			}
			else
			{
				this.FamilyTypes = null;
			}
		}

		private void AddParametersM()
		{

			if (this.SelectedType!=null)
			{
				this.CurrentPage = ApplicationPage.Parameters;
			}
		}

		private void RemoveParameters()
		{
			if (this.SelectedRemove != null)
			{
				if (this.SelectedRemove.Count() > 0)
				{
					var remove = this.SelectedRemove.ToList();
					this.AddParameters = new ObservableCollection<MyParameters>( Utils.CleanList(this.AddParameters.ToList(), this.SelectedRemove.ToList()));
					var list = this.Parameters.ToList();
					list.AddRange(remove);
					this.Parameters = new ObservableCollection<MyParameters>(list.OrderBy(x=>x.GetParameter.Definition.Name));
					this.SelectedRemove = new ObservableCollection<MyParameters>();
				} 
			}
		}

		private void CallParameterPage()
		{
			if (this.SelectedType != null)
			{

			}
		}

		private void GetParameters()
		{
			if (this.SelectedType != null)
			{
				List<MyParameters> parameters = Utils.GetFamilyParameters(this.Doc, this.SelectedType).Select(x=> new MyParameters() { GetParameter = x }).GroupBy(x=>x.GetParameter.Definition.Name).Select(x=>x.FirstOrDefault()).ToList();

				this.Parameters = new ObservableCollection<MyParameters>(parameters.OrderBy(x=>x.Name).ToList());

			}
		}

		private void Ok()
		{
			this.CurrentPage = ApplicationPage.Main;
			if (this.SelectedParameters!= null)
			{
				if (this.SelectedParameters.Count()>0)
				{
					var list = this.AddParameters.ToList();
					list.AddRange(this.SelectedParameters.ToList());

					this.Parameters = new ObservableCollection<MyParameters>(Utils.CleanList(this.Parameters.ToList(), list));

					this.AddParameters = new ObservableCollection<MyParameters>(list);
					this.SelectedParameters = new ObservableCollection<MyParameters>(); 
				}
			}
		}

		private void Export()
		{
			if (this.SelectedType!=null)
			{
				Utils.Export(this.Doc, this.SelectedType, this.AddParameters.ToList().Select(x => x.GetParameter).ToList(), this.ExportCurrentView, this.ZCoordinate);
				this.HideInView = true;
			}
		}

		private void MoveUp()
		{
			if (this.SelectedRemove!=null)
			{
				if (this.SelectedRemove.Count()>0)
				{
					var remove = this.SelectedRemove.ToList();
					var main = this.AddParameters.ToList();

					var mainNew = Utils.OrderListUp(main, remove);

					this.AddParameters = new ObservableCollection<MyParameters>(mainNew);
					
				}
			}
		}
		
		private void MoveDown()
		{
			if (this.SelectedRemove != null)
			{
				if (this.SelectedRemove.Count() > 0)
				{
					var remove = this.SelectedRemove.ToList();
					var main = this.AddParameters.ToList();

					var mainNew = Utils.OrderListDown(main, remove);

					this.AddParameters = new ObservableCollection<MyParameters>(mainNew);

				}
			}
		}

		#endregion

		public struct MyParameters
		{
			public Parameter GetParameter {  get; set; }
			public string Name { get { return this.GetParameter.Definition.Name; } }
		}

	}
}
