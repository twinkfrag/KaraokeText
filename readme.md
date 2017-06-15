カラオケ風に文字色を変化させるテスト


## Configuration

- FontをImport
- Materialを作成
  + shaderをKaraokeTextに
  + BaseColor(変化前の色)
  + PastColor(変化後の色)
  + FontTextureをD&D
- 3D Textを作成
  + MeshRenderer > Material
  + TextMesh > Font
  + KaraokeTextLiner.csをアタッチ
  + KaraokeTextLiner > KaraokeTextMaterial

あとは，TextMeshのTextを適当に入れてKaraokeTextLinerのLerpを変化させれば動きます．


## Known Issue

- 回転させると色変化の範囲が合わなくなる

## Using

### github:adobe-fonts/source-han-sans
SIL Open Font License 1.1
