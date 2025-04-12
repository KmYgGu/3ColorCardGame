using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExcelAsset]
[ExcelAsset(AssetPath = "Resources")]//오브젝트 파일 생성되는 경로를 설정
public class CardList : ScriptableObject
{
	public List<colorCardData_Entity> colorCardData; // Replace 'EntityType' to an actual type that is serializable.
	public List<eventCardData_Entity> eventCardData; // Replace 'EntityType' to an actual type that is serializable.
	public List<CPUDeckData_Entity> CPUDeckData; // Replace 'EntityType' to an actual type that is serializable.
}
