package com.example.exampleapp

import android.graphics.BitmapFactory
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.ImageView
import androidx.fragment.app.Fragment
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import com.example.exampleapp.databinding.ActivityMainBinding
import com.example.exampleapp.databinding.ActivitySecondBinding
import com.example.exampleapp.fragments.Cutout
import com.example.exampleapp.fragments.Filter
import com.example.exampleapp.fragments.Rotate
import com.example.exampleapp.fragments.Scale
import com.google.android.material.bottomnavigation.BottomNavigationView

class SecondActivity : AppCompatActivity() {

    private lateinit var binding: ActivitySecondBinding
    private val rotateFrag = Rotate()
    private val scaleFrag = Scale()
    private val cutFrag = Cutout()
    private val filterFrag = Filter()


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivitySecondBinding.inflate(layoutInflater)
        setContentView(binding.root)
        displayImage()

    }
    private fun displayImage(){
        intent.getParcelableExtra<Uri>(MainActivity.KEY_IMAGE_URI)?.let{imageUri->
            val inputStream = contentResolver.openInputStream(imageUri)
            val bitmap = BitmapFactory.decodeStream(inputStream)
            binding.imageView3.setImageBitmap(bitmap)
            binding.imageView3.visibility = View.VISIBLE

            }
        }
    }

