using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class InventoryController : Controller
{
    public InventoryController(PlayerSession session) : base(session)
    {
        // InventoryController.
    }

    [NetEvent(MessageId.NormalItemRequest)]
	public RpcResult OnNormalItemRequest()
	{
		return Controller.Response(MessageId.NormalItemResponse, new NormalItemResponse
		{
			NormalItemList = 
			{
				new NormalItem
				{
					Id = 50001,
					Count = 1600
				},
				new NormalItem
				{
					Id = 50002,
					Count = 1600
				}
			}
		});
	}

    [NetEvent(MessageId.WeaponItemRequest)]
    public RpcResult OnWeaponItemRequest() {
		
		return Controller.Response(MessageId.WeaponItemResponse, new WeaponItemResponse{
			WeaponItemList = {
				new WeaponItem
				{
					Id = 21010015,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21020015,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030015,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030015,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21040015,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21050015,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21010024,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21020024,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030024,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030024,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21040024,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21050024,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21010013,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21020013,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030013,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030013,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21040013,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21050013,
					WeaponResonLevel = 1
				},new WeaponItem
				{
					Id = 21010012,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21020012,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030012,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030012,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21040012,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21050012,
					WeaponResonLevel = 1
				},new WeaponItem
				{
					Id = 21010011,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21020011,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030011,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030011,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21040011,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21050011,
					WeaponResonLevel = 1
				},new WeaponItem
				{
					Id = 21010023,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21020023,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030023,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21030023,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21040023,
					WeaponResonLevel = 1
				},
				new WeaponItem
				{
					Id = 21050023,
					WeaponResonLevel = 1
				}
			}
		});

	}

    [NetEvent(MessageId.PhantomItemRequest)]
    public RpcResult OnPhantomItemRequest() => Response(MessageId.PhantomItemResponse, new PhantomItemResponse());

    [NetEvent(MessageId.ItemExchangeInfoRequest)]
    public RpcResult OnItemExchangeInfoRequest() => Response(MessageId.ItemExchangeInfoResponse, new ItemExchangeInfoResponse());
}
