using System;
using RimWorld;
using Verse.AI;

namespace Verse
{
	// Token: 0x0200025D RID: 605
	public class HediffComp_EVOR_ReactOnDamage : HediffComp
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0005D108 File Offset: 0x0005B308
		public HediffCompProperties_EVOR_ReactOnDamage Props
		{
			get
			{
				return (HediffCompProperties_EVOR_ReactOnDamage)this.props;
			}
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0005D115 File Offset: 0x0005B315
		public override void Notify_PawnPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
		{
			this.React();
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0005D134 File Offset: 0x0005B334
		private void React()
		{
			if (this.Props.createHediff != null)
			{
				BodyPartRecord part = this.parent.Part;
				if (this.Props.createHediffOn != null)
				{
					part = this.parent.pawn.RaceProps.body.AllParts.FirstOrFallback((BodyPartRecord p) => p.def == this.Props.createHediffOn, null);
				}
				this.parent.pawn.health.AddHediff(this.Props.createHediff, part, null, null);
			}
			if (this.Props.vomit)
			{
				this.parent.pawn.jobs.StartJob(JobMaker.MakeJob(JobDefOf.Vomit), JobCondition.InterruptForced, null, true, true, null, null, false, false);
			}
		}
	}

	public class HediffCompProperties_EVOR_ReactOnDamage : HediffCompProperties
	{
		// Token: 0x06001063 RID: 4195 RVA: 0x0005D0F0 File Offset: 0x0005B2F0
		public HediffCompProperties_EVOR_ReactOnDamage()
		{
			this.compClass = typeof(HediffComp_EVOR_ReactOnDamage);
		}

		// Token: 0x04000BF6 RID: 3062
		public BodyPartDef createHediffOn;

		// Token: 0x04000BF7 RID: 3063
		public HediffDef createHediff;

		// Token: 0x04000BF8 RID: 3064
		public bool vomit;
	}
}
