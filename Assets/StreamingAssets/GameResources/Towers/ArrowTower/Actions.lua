function OnSpawn(tower)
	sum = 0;
	for i = 1,100 do
		sum = sum + tower.stats.GetStat("Damage");
	end
end

function OnAttack(tower, enemy)
end

function OnSell(tower)
end

function OnDestroy(tower)
end