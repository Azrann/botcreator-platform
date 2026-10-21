export interface IMenuItem {
  title: string;
  active?: boolean;
  groupTitle?: boolean;
  badge?: string;
  badgeView?: string;
  routing?: string;
	sub?: IMenuSubItem[];
  subSections: number;
}

export interface IMenuSubItem {
	title?: string;
	routing?: string;
  sectionTitle?: string;
  subSection?: IMenuSubSection[];
}

export interface IMenuSubSection {
  title: string;
  routing: string;
}
