﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace MycoKeyCreator.Application.ViewModel
{
    public class KeyViewModel : OpenControls.Wpf.Utilities.ViewModel.BaseViewModel
    {
        public KeyViewModel(MycoKeyCreator.Library.Database.IKeyManager iKeyManager, MycoKeyCreator.Library.DBObject.Key key)
        {
            IKeyManager = iKeyManager;
            Key = key;
            LoadHeader();
            LoadSpecies();
            LoadAttributes();
        }

        public void LoadHeader()
        {
            Name = Key.name;
            Title = Key.title;
            Description = Key.description;
            Notes = Key.notes;
            Copyright = Key.copyright;
            Publish = Key.Publish;
            LiteratureItems = new System.Collections.ObjectModel.ObservableCollection<Library.DBObject.Literature>(IKeyManager.GetLiteratureEnumeratorForKey(Key.id).OrderBy(n => n.position));
        }

        public void LoadSpecies()
        {
            Species = new System.Collections.ObjectModel.ObservableCollection<Library.DBObject.Species>(
                IKeyManager.GetKeySpeciesEnumerator(Key.id).OrderBy(n => n.name));
            SelectedSpecies = Species.Count > 0 ? Species[0] : null;
        }

        public void LoadAttributes()
        {
            Attributes = new System.Collections.ObjectModel.ObservableCollection<Library.DBObject.Attribute>(
                IKeyManager.GetAttributeEnumeratorForKey(Key.id).OrderBy(n => n.position));
            if (Attributes.Count == 0)
            {
                SelectedAttribute = null;
            }
            else
            {
                SelectedAttribute = Attributes[0];
            }
        }

        public void Save()
        {
            Key.name = Name;
            Key.title = Title;
            Key.description = Description;
            Key.notes = Notes;
            Key.copyright = Copyright;
            Key.Publish = Publish;
            IKeyManager.Update(Key);
        }

        public readonly MycoKeyCreator.Library.Database.IKeyManager IKeyManager;

        public readonly MycoKeyCreator.Library.DBObject.Key Key;

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        private string _notes;
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
                NotifyPropertyChanged("Notes");
            }
        }

        private string _copyright;
        public string Copyright
        {
            get
            {
                return _copyright;
            }
            set
            {
                _copyright = value;
                NotifyPropertyChanged("Copyright");
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<Library.DBObject.Literature> _literatureItems;
        public System.Collections.ObjectModel.ObservableCollection<Library.DBObject.Literature> LiteratureItems
        {
            get
            {
                return _literatureItems;
            }
            set
            {
                _literatureItems = value;
                NotifyPropertyChanged("LiteratureItems");
            }
        }

        private bool _publish;
        public bool Publish
        {
            get
            {
                return _publish;
            }
            set
            {
                _publish = value;
                NotifyPropertyChanged("Publish");
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<MycoKeyCreator.Library.DBObject.Species> _species;
        public System.Collections.ObjectModel.ObservableCollection<MycoKeyCreator.Library.DBObject.Species> Species
        {
            get
            {
                return _species;
            }
            set
            {
                _species = value;
                NotifyPropertyChanged("Species");
            }
        }

        private MycoKeyCreator.Library.DBObject.Species _selectedSpecies;
        public MycoKeyCreator.Library.DBObject.Species SelectedSpecies
        {
            get
            {
                return _selectedSpecies;
            }
            set
            {
                _selectedSpecies = value;
                NotifyPropertyChanged("SelectedSpecies");
            }
        }

        public void DeleteSelectedSpecies()
        {
            if ((SelectedSpecies != null) && IKeyManager.Delete(SelectedSpecies))
            {
                Species.Remove(SelectedSpecies);
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<Library.DBObject.Attribute> _attributes;
        public System.Collections.ObjectModel.ObservableCollection<Library.DBObject.Attribute> Attributes
        {
            get
            {
                return _attributes;
            }
            set
            {
                _attributes = value;
                NotifyPropertyChanged("Attributes");
            }
        }

        private Library.DBObject.Attribute _selectedAttribute;
        public Library.DBObject.Attribute SelectedAttribute
        {
            get
            {
                return _selectedAttribute;
            }
            set
            {
                _selectedAttribute = value;
                NotifyPropertyChanged("SelectedAttribute");
                NotifyPropertyChanged("CanMoveAttributeUp");
                NotifyPropertyChanged("CanMoveAttributeDown");
            }
        }

        public void ReloadAttributes()
        {
            Library.DBObject.Attribute attribute = SelectedAttribute;
            LoadAttributes();
            if (attribute != null)
            {
                SelectedAttribute = (Attributes != null) && (Attributes.Count > 0) ?
                    Attributes.Where(n => attribute.id == n.id).FirstOrDefault() :
                    null;
            }
        }

        public void DeleteSelectedAttribute()
        {
            if ((SelectedAttribute != null) && IKeyManager.Delete(SelectedAttribute))
            {
                Attributes.Remove(SelectedAttribute);
            }
        }

        public bool CanMoveAttributeUp
        {
            get
            {
                if (SelectedAttribute == null)
                {
                    return false;
                }

                int index = Attributes.IndexOf(SelectedAttribute);
                return index > 0;
            }
            set
            {
                NotifyPropertyChanged("CanMoveAttributeUp");
            }
        }

        public bool CanMoveAttributeDown
        {
            get
            {
                if (SelectedAttribute == null)
                {
                    return false;
                }

                int index = Attributes.IndexOf(SelectedAttribute);
                return index < (Attributes.Count - 1);
            }
            set
            {
                NotifyPropertyChanged("CanMoveAttributeDown");
            }
        }

        public void MoveSelectedAttributeUp()
        {
            if (!CanMoveAttributeUp)
            {
                return;
            }

            int index = Attributes.IndexOf(SelectedAttribute);
            SelectedAttribute.position = (short)(index - 1);
            Attributes[index - 1].position = (short)index;
            IKeyManager.Update(Attributes[index - 1]);
            IKeyManager.Update(Attributes[index]);
            LoadAttributes();
            SelectedAttribute = Attributes[index - 1];
        }

        public void MoveSelectedAttributeDown()
        {
            if (!CanMoveAttributeDown)
            {
                return;
            }

            int index = Attributes.IndexOf(SelectedAttribute);
            SelectedAttribute.position = (short)(index + 1);
            Attributes[index + 1].position = (short)index;
            IKeyManager.Update(Attributes[index]);
            IKeyManager.Update(Attributes[index + 1]);
            LoadAttributes();
            SelectedAttribute = Attributes[index + 1];
        }

        public void AddAttribute(Library.DBObject.Attribute attribute)
        {
            attribute.key_id = Key.id;
            IKeyManager.Insert(attribute);
        }

        public void UpdateAttribute(Library.DBObject.Attribute attribute)
        {
            IKeyManager.Update(attribute);
        }
    }
}
