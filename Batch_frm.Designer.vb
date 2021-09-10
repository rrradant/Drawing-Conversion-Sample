<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBatch
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents txtLogfile As System.Windows.Forms.TextBox
	Public WithEvents txtStatus As System.Windows.Forms.TextBox
	Public WithEvents chkDraft As System.Windows.Forms.CheckBox
	Public WithEvents chkAssembly As System.Windows.Forms.CheckBox
	Public WithEvents chkPart As System.Windows.Forms.CheckBox
	Public WithEvents cboFromType As System.Windows.Forms.ComboBox
	Public WithEvents optConvertFrom As System.Windows.Forms.RadioButton
	Public WithEvents cboToType As System.Windows.Forms.ComboBox
	Public WithEvents optConvertTo As System.Windows.Forms.RadioButton
	Public WithEvents optPrintDocuments As System.Windows.Forms.RadioButton
    Public WithEvents grpTypes As System.Windows.Forms.GroupBox
    Public WithEvents optAllFiles As System.Windows.Forms.RadioButton
    Public WithEvents optAllInDirectory As System.Windows.Forms.RadioButton
    Public WithEvents optSelected As System.Windows.Forms.RadioButton
    Public WithEvents grpDocuments As System.Windows.Forms.GroupBox
    Public WithEvents cmdProcess As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents lblPath As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtLogfile = New System.Windows.Forms.TextBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.grpTypes = New System.Windows.Forms.GroupBox()
        Me.buttonSelectNewTemplate = New System.Windows.Forms.Button()
        Me.tbNewTemplatePath = New System.Windows.Forms.TextBox()
        Me.optReplaceBackground = New System.Windows.Forms.RadioButton()
        Me.chkLoadInactive = New System.Windows.Forms.CheckBox()
        Me.grpTemplates = New System.Windows.Forms.GroupBox()
        Me.optDraft = New System.Windows.Forms.RadioButton()
        Me.optSheetmetal = New System.Windows.Forms.RadioButton()
        Me.optPart = New System.Windows.Forms.RadioButton()
        Me.optAssembly = New System.Windows.Forms.RadioButton()
        Me.chkDraft = New System.Windows.Forms.CheckBox()
        Me.chkAssembly = New System.Windows.Forms.CheckBox()
        Me.chkPart = New System.Windows.Forms.CheckBox()
        Me.cboFromType = New System.Windows.Forms.ComboBox()
        Me.optConvertFrom = New System.Windows.Forms.RadioButton()
        Me.cboToType = New System.Windows.Forms.ComboBox()
        Me.optConvertTo = New System.Windows.Forms.RadioButton()
        Me.optPrintDocuments = New System.Windows.Forms.RadioButton()
        Me.grpDocuments = New System.Windows.Forms.GroupBox()
        Me.optAllFiles = New System.Windows.Forms.RadioButton()
        Me.optAllInDirectory = New System.Windows.Forms.RadioButton()
        Me.optSelected = New System.Windows.Forms.RadioButton()
        Me.cmdProcess = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblPath = New System.Windows.Forms.Label()
        Me.lstFiles = New System.Windows.Forms.ListBox()
        Me.btnFolder = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.outputfolder = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpTypes.SuspendLayout()
        Me.grpTemplates.SuspendLayout()
        Me.grpDocuments.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtLogfile
        '
        Me.txtLogfile.AcceptsReturn = True
        Me.txtLogfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLogfile.BackColor = System.Drawing.SystemColors.Window
        Me.txtLogfile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLogfile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLogfile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLogfile.Location = New System.Drawing.Point(7, 518)
        Me.txtLogfile.MaxLength = 0
        Me.txtLogfile.Name = "txtLogfile"
        Me.txtLogfile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLogfile.Size = New System.Drawing.Size(374, 20)
        Me.txtLogfile.TabIndex = 6
        '
        'txtStatus
        '
        Me.txtStatus.AcceptsReturn = True
        Me.txtStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Control
        Me.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStatus.Location = New System.Drawing.Point(7, 544)
        Me.txtStatus.MaxLength = 0
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus.Size = New System.Drawing.Size(456, 20)
        Me.txtStatus.TabIndex = 17
        '
        'grpTypes
        '
        Me.grpTypes.BackColor = System.Drawing.SystemColors.Control
        Me.grpTypes.Controls.Add(Me.buttonSelectNewTemplate)
        Me.grpTypes.Controls.Add(Me.tbNewTemplatePath)
        Me.grpTypes.Controls.Add(Me.optReplaceBackground)
        Me.grpTypes.Controls.Add(Me.chkLoadInactive)
        Me.grpTypes.Controls.Add(Me.grpTemplates)
        Me.grpTypes.Controls.Add(Me.chkDraft)
        Me.grpTypes.Controls.Add(Me.chkAssembly)
        Me.grpTypes.Controls.Add(Me.chkPart)
        Me.grpTypes.Controls.Add(Me.cboFromType)
        Me.grpTypes.Controls.Add(Me.optConvertFrom)
        Me.grpTypes.Controls.Add(Me.cboToType)
        Me.grpTypes.Controls.Add(Me.optConvertTo)
        Me.grpTypes.Controls.Add(Me.optPrintDocuments)
        Me.grpTypes.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTypes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpTypes.Location = New System.Drawing.Point(208, 29)
        Me.grpTypes.Name = "grpTypes"
        Me.grpTypes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grpTypes.Size = New System.Drawing.Size(255, 348)
        Me.grpTypes.TabIndex = 16
        Me.grpTypes.TabStop = False
        Me.grpTypes.Text = "Process Types"
        '
        'buttonSelectNewTemplate
        '
        Me.buttonSelectNewTemplate.Location = New System.Drawing.Point(222, 62)
        Me.buttonSelectNewTemplate.Name = "buttonSelectNewTemplate"
        Me.buttonSelectNewTemplate.Size = New System.Drawing.Size(27, 23)
        Me.buttonSelectNewTemplate.TabIndex = 30
        Me.buttonSelectNewTemplate.Text = "..."
        Me.buttonSelectNewTemplate.UseVisualStyleBackColor = True
        '
        'tbNewTemplatePath
        '
        Me.tbNewTemplatePath.Location = New System.Drawing.Point(6, 64)
        Me.tbNewTemplatePath.Name = "tbNewTemplatePath"
        Me.tbNewTemplatePath.ReadOnly = True
        Me.tbNewTemplatePath.Size = New System.Drawing.Size(212, 20)
        Me.tbNewTemplatePath.TabIndex = 29
        Me.tbNewTemplatePath.Text = "Browse for new template"
        '
        'optReplaceBackground
        '
        Me.optReplaceBackground.BackColor = System.Drawing.SystemColors.Control
        Me.optReplaceBackground.Cursor = System.Windows.Forms.Cursors.Default
        Me.optReplaceBackground.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optReplaceBackground.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optReplaceBackground.Location = New System.Drawing.Point(6, 39)
        Me.optReplaceBackground.Name = "optReplaceBackground"
        Me.optReplaceBackground.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optReplaceBackground.Size = New System.Drawing.Size(145, 17)
        Me.optReplaceBackground.TabIndex = 28
        Me.optReplaceBackground.Text = "Replace Background"
        Me.optReplaceBackground.UseVisualStyleBackColor = False
        '
        'chkLoadInactive
        '
        Me.chkLoadInactive.AutoSize = True
        Me.chkLoadInactive.Enabled = False
        Me.chkLoadInactive.Location = New System.Drawing.Point(126, 321)
        Me.chkLoadInactive.Name = "chkLoadInactive"
        Me.chkLoadInactive.Size = New System.Drawing.Size(90, 18)
        Me.chkLoadInactive.TabIndex = 27
        Me.chkLoadInactive.Text = "Load Inactive"
        Me.chkLoadInactive.UseVisualStyleBackColor = True
        '
        'grpTemplates
        '
        Me.grpTemplates.Controls.Add(Me.optDraft)
        Me.grpTemplates.Controls.Add(Me.optSheetmetal)
        Me.grpTemplates.Controls.Add(Me.optPart)
        Me.grpTemplates.Controls.Add(Me.optAssembly)
        Me.grpTemplates.Location = New System.Drawing.Point(38, 121)
        Me.grpTemplates.Name = "grpTemplates"
        Me.grpTemplates.Size = New System.Drawing.Size(211, 112)
        Me.grpTemplates.TabIndex = 26
        Me.grpTemplates.TabStop = False
        Me.grpTemplates.Text = "Templates"
        '
        'optDraft
        '
        Me.optDraft.AutoSize = True
        Me.optDraft.Location = New System.Drawing.Point(7, 91)
        Me.optDraft.Name = "optDraft"
        Me.optDraft.Size = New System.Drawing.Size(49, 18)
        Me.optDraft.TabIndex = 3
        Me.optDraft.TabStop = True
        Me.optDraft.Text = "Draft"
        Me.optDraft.UseVisualStyleBackColor = True
        '
        'optSheetmetal
        '
        Me.optSheetmetal.AutoSize = True
        Me.optSheetmetal.Location = New System.Drawing.Point(6, 43)
        Me.optSheetmetal.Name = "optSheetmetal"
        Me.optSheetmetal.Size = New System.Drawing.Size(78, 18)
        Me.optSheetmetal.TabIndex = 2
        Me.optSheetmetal.TabStop = True
        Me.optSheetmetal.Text = "Sheetmetal"
        Me.optSheetmetal.UseVisualStyleBackColor = True
        '
        'optPart
        '
        Me.optPart.AutoSize = True
        Me.optPart.Location = New System.Drawing.Point(7, 19)
        Me.optPart.Name = "optPart"
        Me.optPart.Size = New System.Drawing.Size(44, 18)
        Me.optPart.TabIndex = 1
        Me.optPart.TabStop = True
        Me.optPart.Text = "Part"
        Me.optPart.UseVisualStyleBackColor = True
        '
        'optAssembly
        '
        Me.optAssembly.AutoSize = True
        Me.optAssembly.Location = New System.Drawing.Point(7, 67)
        Me.optAssembly.Name = "optAssembly"
        Me.optAssembly.Size = New System.Drawing.Size(73, 18)
        Me.optAssembly.TabIndex = 0
        Me.optAssembly.TabStop = True
        Me.optAssembly.Text = "Assembly"
        Me.optAssembly.UseVisualStyleBackColor = True
        '
        'chkDraft
        '
        Me.chkDraft.BackColor = System.Drawing.SystemColors.Control
        Me.chkDraft.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDraft.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDraft.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDraft.Location = New System.Drawing.Point(6, 322)
        Me.chkDraft.Name = "chkDraft"
        Me.chkDraft.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDraft.Size = New System.Drawing.Size(112, 17)
        Me.chkDraft.TabIndex = 25
        Me.chkDraft.Text = "D&raft documents"
        Me.chkDraft.UseVisualStyleBackColor = False
        '
        'chkAssembly
        '
        Me.chkAssembly.BackColor = System.Drawing.SystemColors.Control
        Me.chkAssembly.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAssembly.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAssembly.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAssembly.Location = New System.Drawing.Point(6, 297)
        Me.chkAssembly.Name = "chkAssembly"
        Me.chkAssembly.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAssembly.Size = New System.Drawing.Size(177, 17)
        Me.chkAssembly.TabIndex = 24
        Me.chkAssembly.Text = "Ass&embly documents"
        Me.chkAssembly.UseVisualStyleBackColor = False
        '
        'chkPart
        '
        Me.chkPart.BackColor = System.Drawing.SystemColors.Control
        Me.chkPart.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPart.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPart.Location = New System.Drawing.Point(6, 274)
        Me.chkPart.Name = "chkPart"
        Me.chkPart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPart.Size = New System.Drawing.Size(181, 17)
        Me.chkPart.TabIndex = 23
        Me.chkPart.Text = "Part/Sheet &Metal documents"
        Me.chkPart.UseVisualStyleBackColor = False
        '
        'cboFromType
        '
        Me.cboFromType.BackColor = System.Drawing.SystemColors.Window
        Me.cboFromType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFromType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFromType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFromType.Location = New System.Drawing.Point(92, 93)
        Me.cboFromType.Name = "cboFromType"
        Me.cboFromType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFromType.Size = New System.Drawing.Size(157, 22)
        Me.cboFromType.TabIndex = 22
        '
        'optConvertFrom
        '
        Me.optConvertFrom.AutoSize = True
        Me.optConvertFrom.BackColor = System.Drawing.SystemColors.Control
        Me.optConvertFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.optConvertFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optConvertFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optConvertFrom.Location = New System.Drawing.Point(6, 94)
        Me.optConvertFrom.Name = "optConvertFrom"
        Me.optConvertFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optConvertFrom.Size = New System.Drawing.Size(91, 18)
        Me.optConvertFrom.TabIndex = 21
        Me.optConvertFrom.Text = "Convert &from:"
        Me.optConvertFrom.UseVisualStyleBackColor = False
        '
        'cboToType
        '
        Me.cboToType.BackColor = System.Drawing.SystemColors.Window
        Me.cboToType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboToType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboToType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboToType.Location = New System.Drawing.Point(94, 248)
        Me.cboToType.Name = "cboToType"
        Me.cboToType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboToType.Size = New System.Drawing.Size(155, 22)
        Me.cboToType.TabIndex = 20
        '
        'optConvertTo
        '
        Me.optConvertTo.AutoSize = True
        Me.optConvertTo.BackColor = System.Drawing.SystemColors.Control
        Me.optConvertTo.Checked = True
        Me.optConvertTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optConvertTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optConvertTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optConvertTo.Location = New System.Drawing.Point(6, 250)
        Me.optConvertTo.Name = "optConvertTo"
        Me.optConvertTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optConvertTo.Size = New System.Drawing.Size(81, 18)
        Me.optConvertTo.TabIndex = 19
        Me.optConvertTo.TabStop = True
        Me.optConvertTo.Text = "Convert &to: "
        Me.optConvertTo.UseVisualStyleBackColor = False
        '
        'optPrintDocuments
        '
        Me.optPrintDocuments.BackColor = System.Drawing.SystemColors.Control
        Me.optPrintDocuments.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPrintDocuments.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPrintDocuments.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintDocuments.Location = New System.Drawing.Point(6, 16)
        Me.optPrintDocuments.Name = "optPrintDocuments"
        Me.optPrintDocuments.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPrintDocuments.Size = New System.Drawing.Size(145, 17)
        Me.optPrintDocuments.TabIndex = 18
        Me.optPrintDocuments.Text = "Print &Draft documents"
        Me.optPrintDocuments.UseVisualStyleBackColor = False
        '
        'grpDocuments
        '
        Me.grpDocuments.BackColor = System.Drawing.SystemColors.Control
        Me.grpDocuments.Controls.Add(Me.optAllFiles)
        Me.grpDocuments.Controls.Add(Me.optAllInDirectory)
        Me.grpDocuments.Controls.Add(Me.optSelected)
        Me.grpDocuments.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDocuments.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpDocuments.Location = New System.Drawing.Point(208, 383)
        Me.grpDocuments.Name = "grpDocuments"
        Me.grpDocuments.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grpDocuments.Size = New System.Drawing.Size(255, 103)
        Me.grpDocuments.TabIndex = 12
        Me.grpDocuments.TabStop = False
        Me.grpDocuments.Text = "Documents to Process "
        '
        'optAllFiles
        '
        Me.optAllFiles.BackColor = System.Drawing.SystemColors.Control
        Me.optAllFiles.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAllFiles.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAllFiles.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAllFiles.Location = New System.Drawing.Point(4, 66)
        Me.optAllFiles.Name = "optAllFiles"
        Me.optAllFiles.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAllFiles.Size = New System.Drawing.Size(229, 32)
        Me.optAllFiles.TabIndex = 9
        Me.optAllFiles.TabStop = True
        Me.optAllFiles.Text = "&All files in selected directory and subdirectories"
        Me.optAllFiles.UseVisualStyleBackColor = False
        '
        'optAllInDirectory
        '
        Me.optAllInDirectory.BackColor = System.Drawing.SystemColors.Control
        Me.optAllInDirectory.Checked = True
        Me.optAllInDirectory.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAllInDirectory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAllInDirectory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAllInDirectory.Location = New System.Drawing.Point(4, 19)
        Me.optAllInDirectory.Name = "optAllInDirectory"
        Me.optAllInDirectory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAllInDirectory.Size = New System.Drawing.Size(179, 18)
        Me.optAllInDirectory.TabIndex = 7
        Me.optAllInDirectory.TabStop = True
        Me.optAllInDirectory.Text = "All &files in selected directory"
        Me.optAllInDirectory.UseVisualStyleBackColor = False
        '
        'optSelected
        '
        Me.optSelected.BackColor = System.Drawing.SystemColors.Control
        Me.optSelected.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSelected.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSelected.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSelected.Location = New System.Drawing.Point(4, 43)
        Me.optSelected.Name = "optSelected"
        Me.optSelected.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSelected.Size = New System.Drawing.Size(121, 17)
        Me.optSelected.TabIndex = 8
        Me.optSelected.TabStop = True
        Me.optSelected.Text = "&Selected files only"
        Me.optSelected.UseVisualStyleBackColor = False
        '
        'cmdProcess
        '
        Me.cmdProcess.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdProcess.BackColor = System.Drawing.SystemColors.Control
        Me.cmdProcess.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdProcess.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProcess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdProcess.Location = New System.Drawing.Point(319, 576)
        Me.cmdProcess.Name = "cmdProcess"
        Me.cmdProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdProcess.Size = New System.Drawing.Size(69, 25)
        Me.cmdProcess.TabIndex = 10
        Me.cmdProcess.Text = "&Process"
        Me.cmdProcess.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(394, 576)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(69, 25)
        Me.cmdCancel.TabIndex = 11
        Me.cmdCancel.Text = "Exit"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'lblPath
        '
        Me.lblPath.AutoSize = True
        Me.lblPath.BackColor = System.Drawing.SystemColors.Control
        Me.lblPath.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPath.Location = New System.Drawing.Point(4, 9)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPath.Size = New System.Drawing.Size(28, 14)
        Me.lblPath.TabIndex = 3
        Me.lblPath.Text = "Path"
        '
        'lstFiles
        '
        Me.lstFiles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstFiles.FormattingEnabled = True
        Me.lstFiles.ItemHeight = 14
        Me.lstFiles.Location = New System.Drawing.Point(7, 64)
        Me.lstFiles.Name = "lstFiles"
        Me.lstFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFiles.Size = New System.Drawing.Size(195, 424)
        Me.lstFiles.TabIndex = 18
        '
        'btnFolder
        '
        Me.btnFolder.Location = New System.Drawing.Point(7, 29)
        Me.btnFolder.Name = "btnFolder"
        Me.btnFolder.Size = New System.Drawing.Size(125, 23)
        Me.btnFolder.TabIndex = 19
        Me.btnFolder.Text = "Browse to Folder"
        Me.btnFolder.UseVisualStyleBackColor = True
        '
        'outputfolder
        '
        Me.outputfolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.outputfolder.Location = New System.Drawing.Point(387, 518)
        Me.outputfolder.Name = "outputfolder"
        Me.outputfolder.Size = New System.Drawing.Size(76, 23)
        Me.outputfolder.TabIndex = 20
        Me.outputfolder.Text = "Browse"
        Me.outputfolder.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 499)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 14)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Output Folder:"
        '
        'frmBatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(472, 613)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.outputfolder)
        Me.Controls.Add(Me.btnFolder)
        Me.Controls.Add(Me.lstFiles)
        Me.Controls.Add(Me.txtLogfile)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.grpTypes)
        Me.Controls.Add(Me.grpDocuments)
        Me.Controls.Add(Me.cmdProcess)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lblPath)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(256, 234)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBatch"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Batch Processing Sample"
        Me.grpTypes.ResumeLayout(False)
        Me.grpTypes.PerformLayout()
        Me.grpTemplates.ResumeLayout(False)
        Me.grpTemplates.PerformLayout()
        Me.grpDocuments.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstFiles As System.Windows.Forms.ListBox
    Friend WithEvents btnFolder As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents grpTemplates As System.Windows.Forms.GroupBox
    Friend WithEvents optDraft As System.Windows.Forms.RadioButton
    Friend WithEvents optSheetmetal As System.Windows.Forms.RadioButton
    Friend WithEvents optPart As System.Windows.Forms.RadioButton
    Friend WithEvents optAssembly As System.Windows.Forms.RadioButton
    Friend WithEvents chkLoadInactive As System.Windows.Forms.CheckBox
    Friend WithEvents outputfolder As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents optReplaceBackground As RadioButton
    Friend WithEvents tbNewTemplatePath As TextBox
    Friend WithEvents buttonSelectNewTemplate As Button
#End Region
End Class