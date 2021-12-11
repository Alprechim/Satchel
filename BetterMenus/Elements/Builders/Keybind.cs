using System;
using GlobalEnums;
using UnityEngine;
using UnityEngine.UI;
using Modding;
using Modding.Menu;
using InControl;
using Satchel.BetterMenus.Config;

namespace Satchel.BetterMenus
{
    /// <summary>
    /// A helper class for creating keybind mapping buttons.
    /// </summary>
    public static class KeybindContent
    {
        /// <summary>
        /// Creates a keybind menu item.
        /// </summary>
        /// <param name="content">The <c>ContentArea</c> to put the keybind item in.</param>
        /// <param name="name">The name of the keybind game object.</param>
        /// <param name="action">The <c>PlayerAction</c> to associate with this keybind.</param>
        /// <param name="config">The configuration options for the keybind item.</param>
        /// <returns></returns>
        public static ContentArea AddCustomKeybind(
            this ContentArea content,
            string name,
            PlayerAction action,
            KeybindConfig config
        ) => content.AddCustomKeybind(name, action, config, out _);

        /// <summary>
        /// Creates a keybind menu item.
        /// </summary>
        /// <param name="content">The <c>ContentArea</c> to put the keybind item in.</param>
        /// <param name="name">The name of the keybind game object.</param>
        /// <param name="action">The <c>PlayerAction</c> to associate with this keybind.</param>
        /// <param name="config">The configuration options for the keybind item.</param>
        /// <param name="mappableKey">The <c>MappablKey</c> component on the created keybind item.</param>
        /// <returns></returns>
        public static ContentArea AddCustomKeybind(
            this ContentArea content,
            string name,
            PlayerAction action,
            KeybindConfig config,
            out MappableKey mappableKey
        )
        {
            var style = config.Style ?? KeybindStyle.VanillaStyle;
            var selectableArea = config.selectableArea ?? SelectableArea.Full;
            // Keybind object
            var keybind = new GameObject($"{name}");
            GameObject.DontDestroyOnLoad(keybind);
            keybind.transform.SetParent(content.ContentObject.transform, false);
            // CanvasRenderer
            keybind.AddComponent<CanvasRenderer>();
            // RectTransform
            var keybindRt = keybind.AddComponent<RectTransform>();
            new RelVector2(new Vector2(650f, 100f)).GetBaseTransformData().Apply(keybindRt);
            content.Layout.ModifyNext(keybindRt);
            

            // Text object
            var text = new GameObject("Text");
            GameObject.DontDestroyOnLoad(text);
            text.transform.SetParent(keybind.transform, false);
            // CanvasRenderer
            text.AddComponent<CanvasRenderer>();
            // RectTransform
            var textRt = text.AddComponent<RectTransform>();
            textRt.sizeDelta = new Vector2(0f, 0f);
            textRt.anchorMin = new Vector2(0f, 0f);
            textRt.anchorMax = new Vector2(1f, 1f);
            textRt.anchoredPosition = new Vector2(0f, 0f);
            textRt.pivot = new Vector2(0.5f, 0.5f);
            // Text
            var labelText = text.AddComponent<Text>();
            labelText.font = MenuResources.TrajanBold;
            labelText.fontSize = style.LabelTextSize;
            labelText.resizeTextMaxSize = style.LabelTextSize;
            labelText.alignment = TextAnchor.MiddleLeft;
            labelText.text = config.Label;
            labelText.supportRichText = true;
            labelText.verticalOverflow = VerticalWrapMode.Overflow;
            labelText.horizontalOverflow = HorizontalWrapMode.Overflow;
            // FixVerticalAlign
            text.AddComponent<FixVerticalAlign>();

            // LeftCursor object
            var cursorL = new GameObject("CursorLeft");
            GameObject.DontDestroyOnLoad(cursorL);
            cursorL.transform.SetParent(keybind.transform, false);
            // CanvasRenderer
            cursorL.AddComponent<CanvasRenderer>();
            // RectTransform
            var cursorLRt = cursorL.AddComponent<RectTransform>();
            cursorLRt.sizeDelta = new Vector2(154f, 112f);
            cursorLRt.pivot = new Vector2(0.5f, 0.5f);
            cursorLRt.anchorMin = new Vector2(0f, 0.5f);
            cursorLRt.anchorMax = new Vector2(0f, 0.5f);
            cursorLRt.anchoredPosition = new Vector2(-52f, 0f);
            cursorLRt.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            // Animator
            var cursorLAnimator = cursorL.AddComponent<Animator>();
            cursorLAnimator.runtimeAnimatorController = MenuResources.MenuCursorAnimator;
            cursorLAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            cursorLAnimator.applyRootMotion = false;
            // Image
            cursorL.AddComponent<Image>();

            // RightCursor object
            var cursorR = new GameObject("CursorRight");
            GameObject.DontDestroyOnLoad(cursorR);
            cursorR.transform.SetParent(keybind.transform, false);
            // CanvasRenderer
            cursorR.AddComponent<CanvasRenderer>();
            // RectTransform
            var cursorRRt = cursorR.AddComponent<RectTransform>();
            cursorRRt.sizeDelta = new Vector2(154f, 112f);
            cursorRRt.pivot = new Vector2(0.5f, 0.5f);
            cursorRRt.anchorMin = new Vector2(1f, 0.5f);
            cursorRRt.anchorMax = new Vector2(1f, 0.5f);
            cursorRRt.anchoredPosition = new Vector2(52f, 0f);
            cursorRRt.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
            // Animator
            var cursorRAnimator = cursorR.AddComponent<Animator>();
            cursorRAnimator.runtimeAnimatorController = MenuResources.MenuCursorAnimator;
            cursorRAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            cursorRAnimator.applyRootMotion = false;
            // Image
            cursorR.AddComponent<Image>();

            // Keymap object
            var keymap = new GameObject("Keymap");
            GameObject.DontDestroyOnLoad(keymap);
            keymap.transform.SetParent(keybind.transform, false);
            // CanvasRenderer
            keymap.AddComponent<CanvasRenderer>();
            // RectTransform
            var keymapRt = keymap.AddComponent<RectTransform>();
            keymapRt.sizeDelta = new Vector2(145.8f, 82.4f);
            keymapRt.anchorMin = new Vector2(1f, 0.5f);
            keymapRt.anchorMax = new Vector2(1f, 0.5f);
            keymapRt.anchoredPosition = new Vector2(0f, 0f);
            keymapRt.pivot = new Vector2(1f, 0.5f);
            // Image
            var keymapImg = keymap.AddComponent<Image>();
            keymapImg.preserveAspect = true;

            // Keymap Text object
            var keymapText = new GameObject("Text");
            GameObject.DontDestroyOnLoad(keymapText);
            keymapText.transform.SetParent(keymap.transform, false);
            // CanvasRenderer
            keymapText.AddComponent<CanvasRenderer>();
            // RectTransform
            var keymapTextRt = keymapText.AddComponent<RectTransform>();
            keymapTextRt.sizeDelta = new Vector2(65f, 60f);
            keymapTextRt.anchorMin = new Vector2(0.5f, 0.5f);
            keymapTextRt.anchorMax = new Vector2(0.5f, 0.5f);
            keymapTextRt.anchoredPosition = new Vector2(32f, 0f);
            keymapTextRt.pivot = new Vector2(0.5f, 0.5f);
            // Text
            var keymapTextText = keymapText.AddComponent<Text>();
            keymapTextText.font = MenuResources.Perpetua;
            // FixVerticalAlign
            keymapText.AddComponent<FixVerticalAlign>().labelFixType = FixVerticalAlign.LabelFixType.KeyMap;

            // MappableKey
            var mapKey = keybind.AddComponent<MappableKey>();
            mapKey.InitCustomActions(action.Owner, action);
            var mkbutton = (MenuSelectable)mapKey;
            mkbutton.cancelAction = (CancelAction)CancelAction.CustomCancelAction;
            mkbutton.customCancelAction = _ =>
            {
                mapKey.AbortRebind();
                config.CancelAction?.Invoke(mapKey);
            };
            content.NavGraph.AddNavigationNode(mapKey);

            mapKey.leftCursor = cursorLAnimator;
            mapKey.rightCursor = cursorRAnimator;

            mapKey.keymapSprite = keymapImg;
            mapKey.keymapText = keymapTextText;

            mapKey.GetBinding();
            mapKey.ShowCurrentBinding();
            mappableKey = mapKey;
            
            if(selectableArea == SelectableArea.ButtonOnly ){
                cursorL.transform.SetParent(keymap.transform, false);
                cursorR.transform.SetParent(keymap.transform, false);

                keymapRt.pivot = new Vector2(0.5f, 0.5f);
                keymapTextRt.anchoredPosition = new Vector2(0f, 0f);
                keymapTextText.alignment = TextAnchor.MiddleCenter;
                ReflectionHelper.SetField( mapKey,"sqrX",0f);
                if(config.Label.Length == 0){
                    textRt.anchorMin = new Vector2(0f, 0f);
                    textRt.anchorMax = new Vector2(0f, 0f);
                }
            }
            return content;
        }
        /// <summary>
        /// Creates a buttonBind menu item.
        /// </summary>
        /// <param name="content">The <c>ContentArea</c> to put the buttonBind item in.</param>
        /// <param name="name">The name of the buttonBind game object.</param>
        /// <param name="action">The <c>PlayerAction</c> to associate with this buttonBind.</param>
        /// <param name="config">The configuration options for the buttonBind item.</param>
        /// <param name="mappableControllerButton">The <c>MappableControllerButton</c> component on the created buttonBind item.</param>
        /// <returns></returns>
        public static ContentArea AddCustomButtonBind(
            this ContentArea content,
            string name,
            PlayerAction action,
            ButtonBindConfig config,
            out MappableControllerButton mappableControllerButton
        )
        {
            var style = config.Style ?? KeybindStyle.VanillaStyle;
            var selectableArea = config.selectableArea ?? SelectableArea.Full;
            // buttonBind object
            var buttonBind = new GameObject($"{name}");
            GameObject.DontDestroyOnLoad(buttonBind);
            buttonBind.transform.SetParent(content.ContentObject.transform, false);
            // CanvasRenderer
            buttonBind.AddComponent<CanvasRenderer>();
            // RectTransform
            var buttonBindRt = buttonBind.AddComponent<RectTransform>();
            new RelVector2(new Vector2(650f, 100f)).GetBaseTransformData().Apply(buttonBindRt);
            content.Layout.ModifyNext(buttonBindRt);
            // MappableControllerButton
            var mapButton = (MappableControllerButton)buttonBind.AddComponent<MappableControllerButton>();
            mapButton.InitCustomActions(action.Owner, action);
            var mkbutton = (MenuSelectable)mapButton;
            mkbutton.cancelAction = (CancelAction)CancelAction.CustomCancelAction;
            mkbutton.customCancelAction = _ =>
            {
                mapButton.AbortRebind();
                config.CancelAction?.Invoke(mapButton);
            };
            content.NavGraph.AddNavigationNode(mapButton);

            // Text object
            var text = new GameObject("Text");
            GameObject.DontDestroyOnLoad(text);
            text.transform.SetParent(buttonBind.transform, false);
            // CanvasRenderer
            text.AddComponent<CanvasRenderer>();
            // RectTransform
            var textRt = text.AddComponent<RectTransform>();
            textRt.sizeDelta = new Vector2(0f, 0f);
            textRt.anchorMin = new Vector2(0f, 0f);
            textRt.anchorMax = new Vector2(1f, 1f);
            textRt.anchoredPosition = new Vector2(0f, 0f);
            textRt.pivot = new Vector2(0.5f, 0.5f);
            // Text
            var labelText = text.AddComponent<Text>();
            labelText.font = MenuResources.TrajanBold;
            labelText.fontSize = style.LabelTextSize;
            labelText.resizeTextMaxSize = style.LabelTextSize;
            labelText.alignment = TextAnchor.MiddleLeft;
            labelText.text = config.Label;
            labelText.supportRichText = true;
            labelText.verticalOverflow = VerticalWrapMode.Overflow;
            labelText.horizontalOverflow = HorizontalWrapMode.Overflow;
            // FixVerticalAlign
            text.AddComponent<FixVerticalAlign>();

            // LeftCursor object
            var cursorL = new GameObject("CursorLeft");
            GameObject.DontDestroyOnLoad(cursorL);
            cursorL.transform.SetParent(buttonBind.transform, false);
            // CanvasRenderer
            cursorL.AddComponent<CanvasRenderer>();
            // RectTransform
            var cursorLRt = cursorL.AddComponent<RectTransform>();
            cursorLRt.sizeDelta = new Vector2(154f, 112f);
            cursorLRt.pivot = new Vector2(0.5f, 0.5f);
            cursorLRt.anchorMin = new Vector2(0f, 0.5f);
            cursorLRt.anchorMax = new Vector2(0f, 0.5f);
            cursorLRt.anchoredPosition = new Vector2(-52f, 0f);
            cursorLRt.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            // Animator
            var cursorLAnimator = cursorL.AddComponent<Animator>();
            cursorLAnimator.runtimeAnimatorController = MenuResources.MenuCursorAnimator;
            cursorLAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            cursorLAnimator.applyRootMotion = false;
            // Image
            cursorL.AddComponent<Image>();
            // Post Component Config
            mapButton.leftCursor = cursorLAnimator;

            // RightCursor object
            var cursorR = new GameObject("CursorRight");
            GameObject.DontDestroyOnLoad(cursorR);
            cursorR.transform.SetParent(buttonBind.transform, false);
            // CanvasRenderer
            cursorR.AddComponent<CanvasRenderer>();
            // RectTransform
            var cursorRRt = cursorR.AddComponent<RectTransform>();
            cursorRRt.sizeDelta = new Vector2(154f, 112f);
            cursorRRt.pivot = new Vector2(0.5f, 0.5f);
            cursorRRt.anchorMin = new Vector2(1f, 0.5f);
            cursorRRt.anchorMax = new Vector2(1f, 0.5f);
            cursorRRt.anchoredPosition = new Vector2(52f, 0f);
            cursorRRt.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
            // Animator
            var cursorRAnimator = cursorR.AddComponent<Animator>();
            cursorRAnimator.runtimeAnimatorController = MenuResources.MenuCursorAnimator;
            cursorRAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            cursorRAnimator.applyRootMotion = false;
            // Image
            cursorR.AddComponent<Image>();
            // Post Component Config
            mapButton.rightCursor = cursorRAnimator;

            // Keymap object
            var keymap = new GameObject("Keymap");
            GameObject.DontDestroyOnLoad(keymap);
            keymap.transform.SetParent(buttonBind.transform, false);
            // CanvasRenderer
            keymap.AddComponent<CanvasRenderer>();
            // RectTransform
            var keymapRt = keymap.AddComponent<RectTransform>();
            keymapRt.sizeDelta = new Vector2(145.8f, 82.4f);
            keymapRt.anchorMin = new Vector2(1f, 0.5f);
            keymapRt.anchorMax = new Vector2(1f, 0.5f);
            keymapRt.anchoredPosition = new Vector2(0f, 0f);
            keymapRt.pivot = new Vector2(1f, 0.5f);
            // Image
            var keymapImg = keymap.AddComponent<Image>();
            keymapImg.preserveAspect = true;
            mapButton.buttonmapSprite = keymapImg;

            // Keymap Text object
            var buttonmapText = new GameObject("Text");
            GameObject.DontDestroyOnLoad(buttonmapText);
            buttonmapText.transform.SetParent(keymap.transform, false);
            // CanvasRenderer
            buttonmapText.AddComponent<CanvasRenderer>();
            // RectTransform
            var buttonmapTextRt = buttonmapText.AddComponent<RectTransform>();
            buttonmapTextRt.sizeDelta = new Vector2(85f, 60f);
            buttonmapTextRt.anchorMin = new Vector2(0f, 0.5f);
            buttonmapTextRt.anchorMax = new Vector2(1f, 0.5f);
            buttonmapTextRt.anchoredPosition = new Vector2(32f, 0f);
            buttonmapTextRt.pivot = new Vector2(0.5f, 0.5f);
            // Text
            var buttonmapTextText = buttonmapText.AddComponent<Text>();
            buttonmapTextText.font = MenuResources.Perpetua;
            buttonmapTextText.fontSize = 35;
            mapButton.buttonmapText = buttonmapTextText;
            // FixVerticalAlign
            buttonmapText.AddComponent<FixVerticalAlign>().labelFixType = FixVerticalAlign.LabelFixType.KeyMap;
            // FakeThrobber to calm NREs
            var throbber = new GameObject("throbber");
            GameObject.DontDestroyOnLoad(throbber);
            throbber.transform.SetParent(keymap.transform, false);
            mapButton.listeningThrobber = throbber.AddComponent<Throbber>();
            ReflectionHelper.SetField( mapButton.listeningThrobber,"sprites",new Sprite[0]);
            
            mapButton.GetBindingPublic();
            mapButton.ShowCurrentBinding();
            mappableControllerButton = mapButton;
            
            if(selectableArea == SelectableArea.ButtonOnly ){
                cursorL.transform.SetParent(keymap.transform, false);
                cursorR.transform.SetParent(keymap.transform, false);
                keymapRt.pivot = new Vector2(0.5f, 0.5f);
                buttonmapTextRt.anchoredPosition = new Vector2(0f, 0f);
                buttonmapTextText.alignment = TextAnchor.MiddleCenter;
                if(config.Label.Length == 0){
                    textRt.anchorMin = new Vector2(0f, 0f);
                    textRt.anchorMax = new Vector2(0f, 0f);
                }
            }
            return content;
        }
    
    }

  
}