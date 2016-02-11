using wServer.networking.cliPackets;
using wServer.networking.svrPackets;

namespace wServer.realm.entities
{
    public partial class Player
    {
        public void PlayerShoot(RealmTime time, PlayerShootPacket pkt)
        {
            System.Diagnostics.Debug.WriteLine(pkt.Position); // Was Commented!
            Item item = XmlDatas.ItemDescs[pkt.ContainerType];
            if (item.ObjectType == Inventory[0].ObjectType || item.ObjectType == Inventory[1].ObjectType)
            {
                var prjDesc = item.Projectiles[0]; //Assume only one
                projectileId = pkt.BulletId;
                Projectile prj = CreateProjectile(prjDesc, item.ObjectType,
                    0,
                    pkt.Time + tickMapping, pkt.Position, pkt.Angle);
                Owner.EnterWorld(prj);
                Owner.BroadcastPacket(new AllyShootPacket()
                {
                    OwnerId = Id,
                    Angle = pkt.Angle,
                    ContainerType = pkt.ContainerType,
                    BulletId = pkt.BulletId
                }, this);
                fames.Shoot(prj);
            }
        }

        public void EnemyHit(RealmTime time, EnemyHitPacket pkt)
        {
            try
            {
                var entity = Owner.GetEntity(pkt.TargetId);
                bool infiniWalling = false;
                if (Owner.Mining)
                {
                    if ((entity is Wall))
                    {
                        infiniWalling = true;
                        Wall w = (entity as Wall);
                        w.HP = 0;
                        GenLogic.GenRandomRoom(Owner, w.X, w.Y, w);
                        for (var tx = -1; tx <= 1; tx++)
                            for (var ty = -1; ty <= 1; ty++)
                            {
                                if (Owner.Map[(int)w.X + tx, (int)w.Y + ty].TileId == 0xff && Owner.Map[(int)w.X + tx, (int)w.Y + ty].ObjId == 0)
                                {
                                    WmapTile tile = Owner.Map[(int)w.X + tx, (int)w.Y + ty];
                                    tile.TileId = Owner.Map[(int)w.X, (int)w.Y].TileId;
                                    Owner.Map[(int)w.X + tx, (int)w.Y + ty] = tile;
                                    Wall e = new Wall(w.ObjectType, XmlDatas.TypeToElement[w.ObjectType]);
                                    e.Move(w.X + tx, w.Y + ty);
                                    Owner.EnterWorld(e);
                                }
                            }
                    }
                }
                Projectile prj = (this as IProjectileOwner).Projectiles[pkt.BulletId];
                prj.Damage = (int)statsMgr.GetAttackDamage(prj.Descriptor.MinDamage, prj.Descriptor.MaxDamage);
                prj.ForceHit(entity, time);
                if (pkt.Killed && !(entity is Wall))
                {
                    client.SendPacket(new UpdatePacket()
                    {
                        Tiles = new UpdatePacket.TileData[0],
                        NewObjects = new ObjectDef[] { entity.ToDefinition() },
                        RemovedObjectIds = new int[] { pkt.TargetId }
                    });
                    clientEntities.Remove(entity);
                }
            }
            catch
            {
            }
        }

        public void OtherHit(RealmTime time, OtherHitPacket pkt)
        {
        }

        public void SquareHit(RealmTime time, SquareHitPacket pkt)
        {
        }

        public void PlayerHit(RealmTime time, PlayerHitPacket pkt)
        {
        }

        public void ShootAck(RealmTime time, ShootAckPacket pkt)
        {
        }
    }
}
