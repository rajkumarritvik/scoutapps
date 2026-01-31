using System.IO;
using System;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QRCoder;
using Avalonia.Platform;

namespace ScoutApp.ViewModels
{
    public enum AutoStartingPosition
    {
        Left,
        Center,
        Right
    }
    public enum Breakdown2026
    {
        None,
        Tipped,
        MechanicalFailure,
        Incapacitated,
        FuelStuck,
        Beached
    }
    public enum Climb2026
    {
        L1,
        L2,
        L3,
        Failed,
        DidNotAttempt
    }

    public enum HeadingButtons
    {
        PreMatch,
        Auto,
        TeleOp,
        Endgame,
        PostMatch
    }

    public enum AlliancePosition
    {
        Red1,
        Red2,
        Red3,
        Blue1,
        Blue2,
        Blue3
    }

    public partial class MainViewModel : ObservableObject
    {
        [RelayCommand]
        private void NextMatch()
        {
            MatchNumber += 1;
            SPosition2026 = null;
            AutoMove = false;
            AutoCoralL4Score = 0;
            AutoCoralL3Score = 0;
            AutoCoralL2Score = 0;
            AutoCoralL1Score = 0;
            AutoCoralL4Miss = 0;
            AutoCoralL3Miss = 0;
            AutoCoralL2Miss = 0;
            AutoCoralL1Miss = 0;
            TeleOpCoralL4Score = 0;
            TeleOpCoralL3Score = 0;
            TeleOpCoralL2Score = 0;
            TeleOpCoralL1Score = 0;
            TeleOpCoralL4Miss = 0;
            TeleOpCoralL3Miss = 0;
            TeleOpCoralL2Miss = 0;
            TeleOpCoralL1Miss = 0;
            AutoProcessorScore = 0;
            AutoProcessorMiss = 0;
            AutoNetScore = 0;
            AutoNetMiss = 0;
            TeleOpProcessorScore = 0;
            TeleOpProcessorMiss = 0;
            TeleOpNetScore = 0;
            TeleOpNetMiss = 0;
            Breakdown = Breakdown2026.None;
            Climb = null;
            ShowSummary = false;
            SelectedHeadingButton = HeadingButtons.PreMatch;
        }

        private static int? GetTeamNumberFromSchedule(int matchNumber, AlliancePosition? alliancePosition)
        {
            if (alliancePosition == null)
                return null;

            return JsonToCSConverter.TryGetTeamNumber(matchNumber, alliancePosition.Value, out int teamNumber)
                ? teamNumber
                : null;
        }

        [RelayCommand]
        private void UpdateTeamNumberFromSchedule()
        {
            int? scheduledTeamNumber = GetTeamNumberFromSchedule(MatchNumber, SelectedAlliancePosition);
            if (scheduledTeamNumber.HasValue)
                TeamNumber = scheduledTeamNumber.Value;
        }

        partial void OnMatchNumberChanged(int value)
        {
            UpdateTeamNumberFromSchedule();
        }

        partial void OnSelectedAlliancePositionChanged(AlliancePosition? value) => UpdateTeamNumberFromSchedule();

        [ObservableProperty]
        private HeadingButtons _SelectedHeadingButton = HeadingButtons.PreMatch;

        [RelayCommand]
        private void HeadingButton(object button)
        {
            if (button is string heading)
            {
                switch (heading)
                {
                    case "PreMatch":
                        SelectedHeadingButton = HeadingButtons.PreMatch;
                        break;
                    case "Auto":
                        SelectedHeadingButton = HeadingButtons.Auto;
                        break;
                    case "TeleOp":
                        SelectedHeadingButton = HeadingButtons.TeleOp;
                        break;
                    case "Endgame":
                        SelectedHeadingButton = HeadingButtons.Endgame;
                        break;
                    case "PostMatch":
                        SelectedHeadingButton = HeadingButtons.PostMatch;
                        break;
                }
            }
        }

        [ObservableProperty]
        private bool showSummary = false;

        [RelayCommand]
        private void ToggleSummary()
        {
            ShowSummary = !ShowSummary;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private string? _ScoutName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _MatchNumber = 1;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int? _TeamNumber;

        [ObservableProperty]
        private AlliancePosition? _SelectedAlliancePosition;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private bool _AutoMove = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoCoralL4Score = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoCoralL3Score = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoCoralL2Score = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoCoralL1Score = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoCoralL4Miss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoCoralL3Miss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoCoralL2Miss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoCoralL1Miss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpCoralL4Score = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpCoralL3Score = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpCoralL2Score = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpCoralL1Score = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpCoralL4Miss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpCoralL3Miss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpCoralL2Miss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpCoralL1Miss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoProcessorScore = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoProcessorMiss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoNetScore = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _AutoNetMiss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpProcessorScore = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpProcessorMiss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpNetScore = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private int _TeleOpNetMiss = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private AutoStartingPosition? _SPosition2026;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private Breakdown2026 _Breakdown = Breakdown2026.None;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Summary))]
        [NotifyPropertyChangedFor(nameof(QRCode1))]
        private Climb2026? _Climb;

        public string Summary
        {
            get
            {
                {
                    return $$"""
Scout Name: {{ScoutName}}
Team Number: {{TeamNumber}}
Match Number: {{MatchNumber}}
Alliance Position: {{SelectedAlliancePosition}}
Auto Starting Position: {{SPosition2026}}
Auto Move: {{AutoMove}}
Auto L4 Score: {{AutoCoralL4Score}}
Auto L3 Score: {{AutoCoralL3Score}}
Auto L2 Score: {{AutoCoralL2Score}}
Auto L1 Score: {{AutoCoralL1Score}}
Auto L4 Miss: {{AutoCoralL4Miss}}
Auto L3 Miss: {{AutoCoralL3Miss}}
Auto L2 Miss: {{AutoCoralL2Miss}}
Auto L1 Miss: {{AutoCoralL1Miss}}
TeleOp L4 Score: {{TeleOpCoralL4Score}}
TeleOp L3 Score: {{TeleOpCoralL3Score}}
TeleOp L2 Score: {{TeleOpCoralL2Score}}
TeleOp L1 Score: {{TeleOpCoralL1Score}}
TeleOp L4 Miss: {{TeleOpCoralL4Miss}}
TeleOp L3 Miss: {{TeleOpCoralL3Miss}}
TeleOp L2 Miss: {{TeleOpCoralL2Miss}}
TeleOp L1 Miss: {{TeleOpCoralL1Miss}}
Auto Processor Score: {{AutoProcessorScore}}
Auto Processor Miss: {{AutoProcessorMiss}}
Auto Net Score: {{AutoNetScore}}
Auto Net Miss: {{AutoNetMiss}}
TeleOp Processor Score: {{TeleOpProcessorScore}}
TeleOp Processor Miss: {{TeleOpProcessorMiss}}
TeleOp Net Score: {{TeleOpNetScore}}
TeleOp Net Miss: {{TeleOpNetMiss}}
Breakdown: {{Breakdown}}
Climb: {{Climb}}
""";
                }
            }
        }

        public Bitmap QRCode1
        {
            get
            {
                string textForQRCode =
                    $$"""
Name-{{ScoutName}}
Team-{{TeamNumber}}
Match-{{MatchNumber}}
APos-{{SelectedAlliancePosition}}
SPos-{{SPosition2026}}
AMove-{{AutoMove}}
AL4-{{AutoCoralL4Score}}
AL3-{{AutoCoralL3Score}}
AL2-{{AutoCoralL2Score}}
AL1-{{AutoCoralL1Score}}
AM4-{{AutoCoralL4Miss}}
AM3-{{AutoCoralL3Miss}}
AM2-{{AutoCoralL2Miss}}
AM1-{{AutoCoralL1Miss}}
TL4-{{TeleOpCoralL4Score}}
TL3-{{TeleOpCoralL3Score}}
TL2-{{TeleOpCoralL2Score}}
TL1-{{TeleOpCoralL1Score}}
TM4-{{TeleOpCoralL4Miss}}
TM3-{{TeleOpCoralL3Miss}}
TM2-{{TeleOpCoralL2Miss}}
TM1-{{TeleOpCoralL1Miss}}
APS-{{AutoProcessorScore}}
APM-{{AutoProcessorMiss}}
ANS-{{AutoNetScore}}
ANM-{{AutoNetMiss}}
TPS-{{TeleOpProcessorScore}}
TPM-{{TeleOpProcessorMiss}}
TNS-{{TeleOpNetScore}}
TNM-{{TeleOpNetMiss}}
BD-{{Breakdown}}
CLB-{{Climb}}
""";

                if (SelectedAlliancePosition == null || SPosition2026 == null || Climb == null)
                {

                    var uri = new Uri("avares://ScoutApp/Assets/cartman.png");
                    using var stream = AssetLoader.Open(uri);
                    return new Bitmap(stream);
                }

                using QRCodeGenerator qrGenerator = new();
                using QRCodeData qrCodeData = qrGenerator.CreateQrCode(textForQRCode, QRCodeGenerator.ECCLevel.Q);
                using PngByteQRCode qrCode = new(qrCodeData);
                byte[] data = qrCode.GetGraphic(20);
                return new Bitmap(new MemoryStream(data));
            }
        }
    }
}
