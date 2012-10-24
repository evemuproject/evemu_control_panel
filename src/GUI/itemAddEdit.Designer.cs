/*------------------------------------------------------------------------------------
LICENSE:
------------------------------------------------------------------------------------
This file is part of EVEmu: EVE Online Server Emulator
Copyright 2006 - 2011 The EVEmu Team
For the latest information visit http://evemu.org
------------------------------------------------------------------------------------
This program is free software; you can redistribute it and/or modify it under
the terms of the GNU Lesser General Public License as published by the Free Software
Foundation; either version 2 of the License, or (at your option) any later
version.

This program is distributed in the hope that it will be useful, but WITHOUT
ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License along with
this program; if not, write to the Free Software Foundation, Inc., 59 Temple
Place - Suite 330, Boston, MA 02111-1307, USA, or go to
http://www.gnu.org/copyleft/lesser.txt.
------------------------------------------------------------------------------------
*/



namespace Evemu_DB_Editor
{
    partial class itemAddEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupSelector = new System.Windows.Forms.ComboBox();
            this.marketGroupSelector = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.itemRefresh = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.chanceOfDuplicating = new System.Windows.Forms.TextBox();
            this.marketGroupID = new System.Windows.Forms.TextBox();
            this.published = new System.Windows.Forms.TextBox();
            this.basePrice = new System.Windows.Forms.TextBox();
            this.raceID = new System.Windows.Forms.TextBox();
            this.capacity = new System.Windows.Forms.TextBox();
            this.portionSize = new System.Windows.Forms.TextBox();
            this.volume = new System.Windows.Forms.TextBox();
            this.radius = new System.Windows.Forms.TextBox();
            this.graphicID = new System.Windows.Forms.TextBox();
            this.mass = new System.Windows.Forms.TextBox();
            this.description = new System.Windows.Forms.TextBox();
            this.groupID = new System.Windows.Forms.TextBox();
            this.typeName = new System.Windows.Forms.TextBox();
            this.typeID1 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.attributeAdd = new System.Windows.Forms.Button();
            this.attributeChange = new System.Windows.Forms.Button();
            this.attributeInt = new System.Windows.Forms.TextBox();
            this.attributeFloat = new System.Windows.Forms.TextBox();
            this.attributeID = new System.Windows.Forms.TextBox();
            this.attributeDescription = new System.Windows.Forms.ComboBox();
            this.itemAttributes = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.itemAttributeContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.isDefault = new System.Windows.Forms.TextBox();
            this.effectID = new System.Windows.Forms.TextBox();
            this.effectDescription = new System.Windows.Forms.ComboBox();
            this.itemEffects = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.itemEffectContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.itemAttributeContext.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.itemEffectContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(422, 500);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupSelector);
            this.tabPage1.Controls.Add(this.marketGroupSelector);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.itemRefresh);
            this.tabPage1.Controls.Add(this.delete);
            this.tabPage1.Controls.Add(this.save);
            this.tabPage1.Controls.Add(this.chanceOfDuplicating);
            this.tabPage1.Controls.Add(this.marketGroupID);
            this.tabPage1.Controls.Add(this.published);
            this.tabPage1.Controls.Add(this.basePrice);
            this.tabPage1.Controls.Add(this.raceID);
            this.tabPage1.Controls.Add(this.capacity);
            this.tabPage1.Controls.Add(this.portionSize);
            this.tabPage1.Controls.Add(this.volume);
            this.tabPage1.Controls.Add(this.radius);
            this.tabPage1.Controls.Add(this.graphicID);
            this.tabPage1.Controls.Add(this.mass);
            this.tabPage1.Controls.Add(this.description);
            this.tabPage1.Controls.Add(this.groupID);
            this.tabPage1.Controls.Add(this.typeName);
            this.tabPage1.Controls.Add(this.typeID1);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(414, 474);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Item Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupSelector
            // 
            this.groupSelector.DropDownWidth = 400;
            this.groupSelector.FormattingEnabled = true;
            this.groupSelector.Location = new System.Drawing.Point(220, 35);
            this.groupSelector.Name = "groupSelector";
            this.groupSelector.Size = new System.Drawing.Size(184, 21);
            this.groupSelector.TabIndex = 36;
            this.groupSelector.SelectedIndexChanged += new System.EventHandler(this.groupSelector_SelectedIndexChanged);
            // 
            // marketGroupSelector
            // 
            this.marketGroupSelector.DropDownWidth = 400;
            this.marketGroupSelector.FormattingEnabled = true;
            this.marketGroupSelector.Location = new System.Drawing.Point(220, 62);
            this.marketGroupSelector.Name = "marketGroupSelector";
            this.marketGroupSelector.Size = new System.Drawing.Size(184, 21);
            this.marketGroupSelector.TabIndex = 35;
            this.marketGroupSelector.SelectedIndexChanged += new System.EventHandler(this.marketGroupSelector_SelectedIndexChanged);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.button5.Location = new System.Drawing.Point(232, 433);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(172, 33);
            this.button5.TabIndex = 34;
            this.button5.Text = "Information/How To";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // itemRefresh
            // 
            this.itemRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.itemRefresh.Location = new System.Drawing.Point(232, 397);
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.Size = new System.Drawing.Size(172, 33);
            this.itemRefresh.TabIndex = 33;
            this.itemRefresh.Text = "Item Refresh";
            this.itemRefresh.UseVisualStyleBackColor = true;
            this.itemRefresh.Click += new System.EventHandler(this.itemRefresh_Click);
            // 
            // delete
            // 
            this.delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.delete.Location = new System.Drawing.Point(232, 358);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(172, 33);
            this.delete.TabIndex = 31;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // save
            // 
            this.save.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.save.Location = new System.Drawing.Point(232, 319);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(172, 33);
            this.save.TabIndex = 30;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // chanceOfDuplicating
            // 
            this.chanceOfDuplicating.Location = new System.Drawing.Point(120, 112);
            this.chanceOfDuplicating.Name = "chanceOfDuplicating";
            this.chanceOfDuplicating.Size = new System.Drawing.Size(94, 20);
            this.chanceOfDuplicating.TabIndex = 29;
            // 
            // marketGroupID
            // 
            this.marketGroupID.Location = new System.Drawing.Point(120, 62);
            this.marketGroupID.Name = "marketGroupID";
            this.marketGroupID.ReadOnly = true;
            this.marketGroupID.Size = new System.Drawing.Size(94, 20);
            this.marketGroupID.TabIndex = 28;
            // 
            // published
            // 
            this.published.Location = new System.Drawing.Point(120, 88);
            this.published.Name = "published";
            this.published.Size = new System.Drawing.Size(94, 20);
            this.published.TabIndex = 27;
            // 
            // basePrice
            // 
            this.basePrice.Location = new System.Drawing.Point(120, 445);
            this.basePrice.Name = "basePrice";
            this.basePrice.Size = new System.Drawing.Size(94, 20);
            this.basePrice.TabIndex = 26;
            // 
            // raceID
            // 
            this.raceID.Location = new System.Drawing.Point(120, 419);
            this.raceID.Name = "raceID";
            this.raceID.Size = new System.Drawing.Size(94, 20);
            this.raceID.TabIndex = 25;
            // 
            // capacity
            // 
            this.capacity.Location = new System.Drawing.Point(120, 368);
            this.capacity.Name = "capacity";
            this.capacity.Size = new System.Drawing.Size(94, 20);
            this.capacity.TabIndex = 24;
            // 
            // portionSize
            // 
            this.portionSize.Location = new System.Drawing.Point(120, 393);
            this.portionSize.Name = "portionSize";
            this.portionSize.Size = new System.Drawing.Size(94, 20);
            this.portionSize.TabIndex = 23;
            // 
            // volume
            // 
            this.volume.Location = new System.Drawing.Point(120, 342);
            this.volume.Name = "volume";
            this.volume.Size = new System.Drawing.Size(94, 20);
            this.volume.TabIndex = 22;
            // 
            // radius
            // 
            this.radius.Location = new System.Drawing.Point(120, 290);
            this.radius.Name = "radius";
            this.radius.Size = new System.Drawing.Size(94, 20);
            this.radius.TabIndex = 21;
            // 
            // graphicID
            // 
            this.graphicID.Location = new System.Drawing.Point(120, 264);
            this.graphicID.Name = "graphicID";
            this.graphicID.Size = new System.Drawing.Size(94, 20);
            this.graphicID.TabIndex = 20;
            // 
            // mass
            // 
            this.mass.Location = new System.Drawing.Point(120, 316);
            this.mass.Name = "mass";
            this.mass.Size = new System.Drawing.Size(94, 20);
            this.mass.TabIndex = 19;
            // 
            // description
            // 
            this.description.Location = new System.Drawing.Point(120, 138);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(284, 120);
            this.description.TabIndex = 18;
            // 
            // groupID
            // 
            this.groupID.Location = new System.Drawing.Point(120, 35);
            this.groupID.Name = "groupID";
            this.groupID.ReadOnly = true;
            this.groupID.Size = new System.Drawing.Size(94, 20);
            this.groupID.TabIndex = 17;
            // 
            // typeName
            // 
            this.typeName.Location = new System.Drawing.Point(220, 9);
            this.typeName.Name = "typeName";
            this.typeName.Size = new System.Drawing.Size(184, 20);
            this.typeName.TabIndex = 16;
            // 
            // typeID1
            // 
            this.typeID1.Location = new System.Drawing.Point(120, 9);
            this.typeID1.Name = "typeID1";
            this.typeID1.ReadOnly = true;
            this.typeID1.Size = new System.Drawing.Size(94, 20);
            this.typeID1.TabIndex = 15;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 396);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 13);
            this.label17.TabIndex = 14;
            this.label17.Text = "Portion Size:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 448);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Base Price:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 319);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Mass:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 422);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Race ID:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 141);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Description:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Published:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Group ID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 371);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Capacity:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Market Group:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Graphic ID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Chance Of Duplicating:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 345);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Volume:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Radius:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Item:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.attributeAdd);
            this.tabPage2.Controls.Add(this.attributeChange);
            this.tabPage2.Controls.Add(this.attributeInt);
            this.tabPage2.Controls.Add(this.attributeFloat);
            this.tabPage2.Controls.Add(this.attributeID);
            this.tabPage2.Controls.Add(this.attributeDescription);
            this.tabPage2.Controls.Add(this.itemAttributes);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(414, 474);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Item Attributes";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // attributeAdd
            // 
            this.attributeAdd.Location = new System.Drawing.Point(297, 447);
            this.attributeAdd.Name = "attributeAdd";
            this.attributeAdd.Size = new System.Drawing.Size(52, 23);
            this.attributeAdd.TabIndex = 38;
            this.attributeAdd.Text = "Add";
            this.attributeAdd.UseVisualStyleBackColor = true;
            this.attributeAdd.Click += new System.EventHandler(this.attributeAdd_Click);
            // 
            // attributeChange
            // 
            this.attributeChange.Location = new System.Drawing.Point(355, 447);
            this.attributeChange.Name = "attributeChange";
            this.attributeChange.Size = new System.Drawing.Size(52, 23);
            this.attributeChange.TabIndex = 37;
            this.attributeChange.Text = "Change";
            this.attributeChange.UseVisualStyleBackColor = true;
            this.attributeChange.Click += new System.EventHandler(this.attributeChange_Click);
            // 
            // attributeInt
            // 
            this.attributeInt.Location = new System.Drawing.Point(175, 448);
            this.attributeInt.Name = "attributeInt";
            this.attributeInt.Size = new System.Drawing.Size(54, 20);
            this.attributeInt.TabIndex = 36;
            // 
            // attributeFloat
            // 
            this.attributeFloat.Location = new System.Drawing.Point(235, 448);
            this.attributeFloat.Name = "attributeFloat";
            this.attributeFloat.Size = new System.Drawing.Size(54, 20);
            this.attributeFloat.TabIndex = 35;
            // 
            // attributeID
            // 
            this.attributeID.Location = new System.Drawing.Point(6, 448);
            this.attributeID.Name = "attributeID";
            this.attributeID.ReadOnly = true;
            this.attributeID.Size = new System.Drawing.Size(47, 20);
            this.attributeID.TabIndex = 34;
            // 
            // attributeDescription
            // 
            this.attributeDescription.FormattingEnabled = true;
            this.attributeDescription.Location = new System.Drawing.Point(59, 447);
            this.attributeDescription.Name = "attributeDescription";
            this.attributeDescription.Size = new System.Drawing.Size(110, 21);
            this.attributeDescription.TabIndex = 33;
            this.attributeDescription.SelectedIndexChanged += new System.EventHandler(this.attributeDescription_SelectedIndexChanged);
            // 
            // itemAttributes
            // 
            this.itemAttributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.itemAttributes.ContextMenuStrip = this.itemAttributeContext;
            this.itemAttributes.FullRowSelect = true;
            this.itemAttributes.HideSelection = false;
            this.itemAttributes.Location = new System.Drawing.Point(6, 6);
            this.itemAttributes.MultiSelect = false;
            this.itemAttributes.Name = "itemAttributes";
            this.itemAttributes.Size = new System.Drawing.Size(399, 435);
            this.itemAttributes.TabIndex = 12;
            this.itemAttributes.UseCompatibleStateImageBehavior = false;
            this.itemAttributes.View = System.Windows.Forms.View.Details;
            this.itemAttributes.SelectedIndexChanged += new System.EventHandler(this.itemAttributes_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "AttributeID";
            this.columnHeader1.Width = 62;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Attribute";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Integer";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Float";
            this.columnHeader4.Width = 50;
            // 
            // itemAttributeContext
            // 
            this.itemAttributeContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.itemAttributeContext.Name = "itemAttributeContext";
            this.itemAttributeContext.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.isDefault);
            this.tabPage3.Controls.Add(this.effectID);
            this.tabPage3.Controls.Add(this.effectDescription);
            this.tabPage3.Controls.Add(this.itemEffects);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(414, 474);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Item Effects";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Enter += new System.EventHandler(this.tabPage3_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 446);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(356, 446);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 23);
            this.button2.TabIndex = 44;
            this.button2.Text = "Change";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // isDefault
            // 
            this.isDefault.Location = new System.Drawing.Point(254, 446);
            this.isDefault.Name = "isDefault";
            this.isDefault.Size = new System.Drawing.Size(38, 20);
            this.isDefault.TabIndex = 43;
            this.isDefault.Text = "0";
            // 
            // effectID
            // 
            this.effectID.Location = new System.Drawing.Point(7, 447);
            this.effectID.Name = "effectID";
            this.effectID.ReadOnly = true;
            this.effectID.Size = new System.Drawing.Size(47, 20);
            this.effectID.TabIndex = 41;
            // 
            // effectDescription
            // 
            this.effectDescription.FormattingEnabled = true;
            this.effectDescription.Location = new System.Drawing.Point(60, 446);
            this.effectDescription.Name = "effectDescription";
            this.effectDescription.Size = new System.Drawing.Size(188, 21);
            this.effectDescription.TabIndex = 40;
            this.effectDescription.SelectedIndexChanged += new System.EventHandler(this.effectDescription_SelectedIndexChanged);
            // 
            // itemEffects
            // 
            this.itemEffects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.itemEffects.ContextMenuStrip = this.itemEffectContext;
            this.itemEffects.FullRowSelect = true;
            this.itemEffects.HideSelection = false;
            this.itemEffects.Location = new System.Drawing.Point(7, 5);
            this.itemEffects.MultiSelect = false;
            this.itemEffects.Name = "itemEffects";
            this.itemEffects.Size = new System.Drawing.Size(399, 435);
            this.itemEffects.TabIndex = 39;
            this.itemEffects.UseCompatibleStateImageBehavior = false;
            this.itemEffects.View = System.Windows.Forms.View.Details;
            this.itemEffects.SelectedIndexChanged += new System.EventHandler(this.itemEffects_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "EffectID";
            this.columnHeader5.Width = 62;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Effect";
            this.columnHeader6.Width = 253;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Is default";
            // 
            // itemEffectContext
            // 
            this.itemEffectContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem1});
            this.itemEffectContext.Name = "itemEffectContext";
            this.itemEffectContext.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // itemAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 516);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "itemAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Name: ";
            this.Load += new System.EventHandler(this.itemAddEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.itemAttributeContext.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.itemEffectContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox chanceOfDuplicating;
        private System.Windows.Forms.TextBox marketGroupID;
        private System.Windows.Forms.TextBox published;
        private System.Windows.Forms.TextBox basePrice;
        private System.Windows.Forms.TextBox raceID;
        private System.Windows.Forms.TextBox capacity;
        private System.Windows.Forms.TextBox portionSize;
        private System.Windows.Forms.TextBox volume;
        private System.Windows.Forms.TextBox radius;
        private System.Windows.Forms.TextBox graphicID;
        private System.Windows.Forms.TextBox mass;
        private System.Windows.Forms.TextBox description;
        private System.Windows.Forms.TextBox groupID;
        private System.Windows.Forms.TextBox typeName;
        private System.Windows.Forms.TextBox typeID1;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button itemRefresh;
        private System.Windows.Forms.Button button5;
        protected System.Windows.Forms.ListView itemAttributes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button attributeAdd;
        private System.Windows.Forms.Button attributeChange;
        private System.Windows.Forms.TextBox attributeInt;
        private System.Windows.Forms.TextBox attributeFloat;
        private System.Windows.Forms.TextBox attributeID;
        private System.Windows.Forms.ComboBox attributeDescription;
        private System.Windows.Forms.ComboBox marketGroupSelector;
        private System.Windows.Forms.ComboBox groupSelector;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox isDefault;
        private System.Windows.Forms.TextBox effectID;
        private System.Windows.Forms.ComboBox effectDescription;
        protected System.Windows.Forms.ListView itemEffects;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ContextMenuStrip itemAttributeContext;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip itemEffectContext;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
    }
}