using System;
using GameServer.Controllers;
using GameServer.Controllers.Attributes;
using GameServer.Controllers.Gacha;
using GameServer.Network;
using GameServer.Network.Messages;
using Google.Protobuf.Collections;
using Protocol;


namespace GameServer.Controllers;
internal class GachaController : Controller
{
	private readonly GachaPool standardBanner = new GachaPool(1, "Beginner", limited: false, 60, 10, 80, new float[3] { 93.2f, 6f, 0.8f });

	public GachaController(PlayerSession session)
		: base(session)
	{
		standardBanner.AddItemsThree(new int[3] { 21050023, 21020023, 21010023 });
		standardBanner.AddItemsFour(new int[9] { 1602, 1204, 1303, 1403, 1103, 1202, 1102, 1601, 1402 });
		standardBanner.AddItemsFive(new int[7] { 1503, 1301, 1203, 1104, 1405, 1404, 1302 });
	}

	[NetEvent(MessageId.GachaRequest)]
	public RpcResult OnGachaRequest(GachaRequest request)
	{
        GachaResponse response = new()
        {
            ErrorCode = (int)ErrorCode.Success
        };
        for (int i = 0; i < request.GachaTimes; i++)
        {
            (int, int) gachaResult = standardBanner.DoPull();
            response.GachaResults.Add(new GachaResult()
            {
                GachaReward = new GachaReward
                {
                    ItemId = gachaResult.Item1,
                    ItemCount = 1
                },
                ExtraRewards =
                {
                    new GachaReward { ItemId = 50003, ItemCount = (int)Math.Pow(2, gachaResult.Item2) },
                    new GachaReward { ItemId = 50004, ItemCount = gachaResult.Item2 <= 3 ? 1 : gachaResult.Item2 * 5 },
                }
            });
        }

        return Response(MessageId.GachaResponse, response);
	}

	[NetEvent(MessageId.GachaUsePoolRequest)]
	public RpcResult OnGachaUsePoolRequest(GachaUsePoolRequest request)
	{
		return Controller.Response<GachaUsePoolResponse>((MessageId)12057, new GachaUsePoolResponse
		{
			ErrorCode = (int)ErrorCode.Success
		});
	}

	[NetEvent(MessageId.GachaInfoRequest)]
	public RpcResult OnGachaInfoRequest()
	{
		return Response(MessageId.GachaInfoResponse, new GachaInfoResponse()
        {
            DailyTotalLeftTimes = 99999,
            ErrorCode = 0,
            RecordId = "eblan",
            GachaInfos =
            {
                CreateGachaInfo(1, "Novice_Convene", 1, "1"), // Beginner
		        CreateGachaInfo(2, "Character_Permanent_Convene", 2, "2"), // Standart
		        CreateGachaInfo(100001, "Character_Event_Convene", 100001, "3"), // Jiyan
                CreateGachaInfo(100002, "Character_Event_Convene", 100002, "4"), // Yinlin
                CreateGachaInfo(200001, "Weapon_Event_Convene", 200001, "5"), // Verdant Summit
		        CreateGachaInfo(200002, "Weapon_Event_Convene", 200002, "6") // Stringmaster
            }
        });
	}

	private GachaInfo CreateGachaInfo(int id, string resourceId, int sort, string poolUrl)
    {
        return new GachaInfo
        {
            Id = id,
            TodayTimes = 0,
            TotalTimes = 0,
            ItemId = 50002,
            GachaConsumes =
        {
            new GachaConsume { Times = 1, Consume = 160 },
            new GachaConsume { Times = 10, Consume = 160 }
        },
            UsePoolId = id,
            Pools =
        {
            new GachaPoolInfo
            {
                Id = id,
                BeginTime = 0,
                EndTime = 1707984518,
                Sort = sort,
                Urls = { poolUrl }
            }
        },
            BeginTime = 0,
            EndTime = 1707984518,
            DailyLimitTimes = 9999,
            TotalLimitTimes = 9999,
            ResourcesId = resourceId,
            Sort = sort
        };
    }
}
